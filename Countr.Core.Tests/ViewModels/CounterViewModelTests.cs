using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using Countr.Core.ViewModels;
using Moq;
using MvvmCross.Core.Navigation;
using NUnit.Framework;

namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    public class CounterViewModelTests
    {
        Mock<ICountersService> countersService;
        Mock<IMvxNavigationService> navigationService;
        CounterViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            countersService = new Mock<ICountersService>();
            navigationService = new Mock<IMvxNavigationService>();
            viewModel = new CounterViewModel(countersService.Object, navigationService.Object);
            viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
        }

        [Test]
        public void Name_ComesFromCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            // Act
            viewModel.Prepare(counter);
            // Assert
            Assert.AreEqual(counter.Name, viewModel.Name);
        }

        [Test]
        public void Count_ComesFromCounter()
        {
            // Arrange
            var counter = new Counter { Count = 4 };

            // Act
            viewModel.Prepare(counter);

            // Assert
            Assert.AreEqual(counter.Count, viewModel.Count);
        }

        [Test]
        public void SettingName_RaisesPropertyChanged()
        {
            // Arrange
            var propertyChangedRaised = false;
            viewModel.PropertyChanged +=
               (s, e) => propertyChangedRaised = (e.PropertyName == "Name");
            viewModel.Prepare(new Counter());

            // Act
            viewModel.Name = "A Counter";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [Test]
        public async Task IncrementCounter_IncrementsTheCounter()
        {
            // Act
            await viewModel.IncrementCommand.ExecuteAsync();
            // Assert
            countersService.Verify(s => s.IncrementCounter(It.IsAny<Counter>()));
        }

        [Test]
        public async Task IncrementCounter_RaisesPropertyChanged()
        {
            // Arrange
            var propertyChangedRaised = false;
            viewModel.PropertyChanged +=
               (s, e) => propertyChangedRaised = (e.PropertyName == "Count");
            // Act
            await viewModel.IncrementCommand.ExecuteAsync();
            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [Test]
        public async Task DeleteCommand_DeletesTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Prepare(counter);

            // Act
            await viewModel.DeleteCommand.ExecuteAsync();

            // Assert
            countersService.Verify(c => c.DeleteCounter(counter));
        }

        [Test]
        public async Task SaveCommand_SavesTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Prepare(counter);
            // Act
            await viewModel.SaveCommand.ExecuteAsync();
            // Assert
            countersService.Verify(c => c.AddNewCounter("A Counter"));
            navigationService.Verify(n => n.Close(viewModel));
        }

        [Test]
        public void CancelCommand_DoesntSaveTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Prepare(counter);
            // Act
            viewModel.CancelCommand.Execute();
            // Assert
            countersService.Verify(c => c.AddNewCounter(It.IsAny<string>()), Times.Never());
            navigationService.Verify(n => n.Close(viewModel));
        }
    }
}
