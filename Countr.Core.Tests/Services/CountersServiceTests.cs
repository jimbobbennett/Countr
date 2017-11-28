using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Countr.Core.Services;
using Moq;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;

namespace Countr.Core.Tests.Services
{
    [TestFixture]
    public class CountersServiceTests
    {
        Mock<ICountersRepository> repo;
        Mock<IMvxMessenger> messenger;
        ICountersService service;

        [SetUp]
        public void SetUp()
        {
            repo = new Mock<ICountersRepository>();
            messenger = new Mock<IMvxMessenger>();
            service = new CountersService(repo.Object, messenger.Object);
        }

        [Test]
        public async Task IncrementCounter_IncrementsTheCounter()
        {
            var counter = new Counter { Count = 0 };
            await service.IncrementCounter(counter);
            Assert.AreEqual(1, counter.Count);
        }

        [Test]
        public async Task IncrementCounter_SavesTheIncrementedCounter()
        {
            var counter = new Counter { Count = 0 };
            await service.IncrementCounter(counter);
            repo.Verify(r => r.Save(It.Is<Counter>(c => c.Count == 1)),
            Times.Once());
        }

        [Test]
        public async Task GetAllCounters_ReturnsAllCountersFromTheRepository()
        {
            var counters = new List<Counter>
           {
              new Counter {Name = "Counter1" },
              new Counter {Name = "Counter2" }
           };
            repo.Setup(r => r.GetAll()).ReturnsAsync(counters);
            var results = await service.GetAllCounters(); CollectionAssert.AreEqual(results, counters);
        }

        [Test]
        public async Task DeleteCounter_PublishesAMessage()
        {
            // Act
            await service.DeleteCounter(new Counter());
            // Assert
            messenger.Verify(m => m.Publish(It.IsAny<CountersChangedMessage>()));
        }
    }
}