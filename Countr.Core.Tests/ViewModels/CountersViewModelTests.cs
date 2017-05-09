using Countr.Core.Services;
using Countr.Core.ViewModels;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using Countr.Core.Models;
using MvvmCross.Plugins.Messenger;
using System;

namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    public class CountersViewModelTests : MvxBaseUnitTest
    {
        private Mock<ICountersService> mockCountersService;
        private CountersViewModel viewModel;
        private Mock<IMvxMessenger> messenger;
        private Action<CountersChangedMessage> publishAction;

        [SetUp]
        public void SetUp()
        {
            // Setup the MvxBaseUnitTest so that we can test navigation
            base.SetUpTests();

            mockCountersService = new Mock<ICountersService>();
            messenger = new Mock<IMvxMessenger>();
            messenger.Setup(m => m.SubscribeOnMainThread
                             (It.IsAny<Action<CountersChangedMessage>>(),
                              It.IsAny<MvxReference>(),
                              It.IsAny<string>()))
                      .Callback<Action<CountersChangedMessage>,
                                MvxReference,
                                string>((a, m, s) => publishAction = a);

            viewModel = new CountersViewModel(mockCountersService.Object,
                                               messenger.Object);
        }

        [Test]
        public void ReceivedMessage_LoadsCounters()
        {
            // Act
            publishAction.Invoke(new CountersChangedMessage(this));

            // Assert
            mockCountersService.Verify(s => s.GetAllCounters());
        }

        [Test]
        public async Task LoadCounters_CreatesCounters()
        {
            // Arrange
            var counters = new List<Counter>
         {
            new Counter{Name = "Counter1", Count=0},
            new Counter{Name = "Counter2", Count=4},
         };
            mockCountersService.Setup(c => c.GetAllCounters())
                                .ReturnsAsync(counters);

            // Act
            await viewModel.LoadCounters();

            // Assert
            Assert.AreEqual(2, viewModel.Counters.Count);
            Assert.AreEqual("Counter1", viewModel.Counters[0].Name);
            Assert.AreEqual(0, viewModel.Counters[0].Count);
            Assert.AreEqual("Counter2", viewModel.Counters[1].Name);
            Assert.AreEqual(4, viewModel.Counters[1].Count);
        }

        [Test]
        public void ShowAddNewCounterCommand_ShowsCounterVieModel()
        {
            // Act
            viewModel.ShowAddNewCounterCommand.Execute(null);

            // Assert
            AssertShowViewModel<CounterViewModel>();
        }
    }
}
