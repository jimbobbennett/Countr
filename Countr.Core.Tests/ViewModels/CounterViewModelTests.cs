using NUnit.Framework;
using Countr.Core.ViewModels;
using Countr.Core.Models;
using Moq;
using Countr.Core.Services;
using System.Threading.Tasks;

namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    public class CounterViewModelTests
    {
        private CounterViewModel viewModel;
        private Mock<ICountersService> mockCountersService;

        [SetUp]
        public void SetUp()
        {
            mockCountersService = new Mock<ICountersService>();
            viewModel = new CounterViewModel(mockCountersService.Object);
            viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
        }

        [Test]
        public async Task IncrementCounter_IncrementsTheCounter()
        {
            // Act
            await viewModel.IncrementCommand.ExecuteAsync();

            // Assert
            mockCountersService.Verify(s => s.IncrementCounter(It.IsAny<Counter>()));
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
        public void Name_ComesFromCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };

            // Act
            viewModel.Init(counter);

            // Assert
            Assert.AreEqual(counter.Name, viewModel.Name);
        }

        [Test]
        public void Count_ComesFromCounter()
        {
            // Arrange
            var counter = new Counter { Count = 4 };

            // Act
            viewModel.Init(counter);

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
            viewModel.Init(new Counter());

            // Act
            viewModel.Name = "A Counter";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [Test]
        public void DeleteCommand_DeletesTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Init(counter);

            // Act
            viewModel.DeleteCommand.Execute();

            // Assert
            mockCountersService.Verify(c => c.DeleteCounter(counter));
        }
    }
}
