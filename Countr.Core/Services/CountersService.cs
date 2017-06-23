using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Microsoft.Azure.Mobile.Analytics;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.Services
{
    public class CountersService : ICountersService
    {
        readonly ICountersRepository repository;
        readonly IMvxMessenger messenger;

        public CountersService(ICountersRepository repository, IMvxMessenger messenger)
        {
            this.repository = repository;
            this.messenger = messenger;
        }

        public async Task<Counter> AddNewCounter(string name)
        {
            var props = new Dictionary<string, string>();
            props.Add("Counter Name", name);
            Analytics.TrackEvent("Add new counter", props);

            var counter = new Counter { Name = name };
            await repository.Save(counter);
            messenger.Publish(new CountersChangedMessage(this));
            return counter;
        }

        public async Task<IEnumerable<Counter>> GetAllCounters()
        {
            return await repository.GetAll();
        }

        public async Task DeleteCounter(Counter counter)
        {
            var props = new Dictionary<string, string>();
            props.Add("Counter Name", counter.Name);
            Analytics.TrackEvent("Delete counter", props);

            await repository.Delete(counter);
            messenger.Publish(new CountersChangedMessage(this));
        }

        public async Task IncrementCounter(Counter counter)
        {
            // Uncomment this to get a crash that shows up in Mobile Center
            // throw new System.Exception("Crash");

            var props = new Dictionary<string, string>();
            props.Add("Counter Name", counter.Name);
            props.Add("Count", counter.Count.ToString());
            Analytics.TrackEvent("Delete counter", props);

            counter.Count += 1;
            await repository.Save(counter);
        }
    }
}