using Countr.Core.Services;
using Countr.Core.ViewModels;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using Countr.Core.Models;
using MvvmCross.Plugins.Messenger;
using System;
using MvvmCross.Core.Navigation;

namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    public class CountersViewModelTests
    {
        Mock<ICountersService> mockCountersService;
        Mock<IMvxNavigationService> mockNavigationService;
        CountersViewModel viewModel;
        Mock<IMvxMessenger> messenger;
        Action<CountersChangedMessage> publishAction;

        [SetUp]
        public void SetUp()
        {
            mockCountersService = new Mock<ICountersService>();
            mockNavigationService = new Mock<IMvxNavigationService>();
            messenger = new Mock<IMvxMessenger>();
            messenger.Setup(m => m.SubscribeOnMainThread
                             (It.IsAny<Action<CountersChangedMessage>>(),
                              It.IsAny<MvxReference>(),
                              It.IsAny<string>()))
                      .Callback<Action<CountersChangedMessage>,
                                MvxReference,
                                string>((a, m, s) => publishAction = a);

            viewModel = new CountersViewModel(mockCountersService.Object,
                                              messenger.Object,
                                              mockNavigationService.Object);
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
        public async Task ShowAddNewCounterCommand_ShowsCounterVieModel()
        {
            // Act
            await viewModel.ShowAddNewCounterCommand.ExecuteAsync();

            // Assert
            mockNavigationService.Verify(n => n.Navigate<CounterViewModel, Counter>(It.IsAny<Counter>(), null));
        }
    }
}
