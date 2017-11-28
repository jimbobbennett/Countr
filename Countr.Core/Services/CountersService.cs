using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.Services
{
    public class CountersService : ICountersService
    {
        readonly ICountersRepository repository;
        readonly IMvxMessenger messenger;

        public CountersService(ICountersRepository repository,
                               IMvxMessenger messenger)
        {
            this.messenger = messenger;
            this.repository = repository;
        }

        public async Task<Counter> AddNewCounter(string name)
        {
            var counter = new Counter { Name = name };
            await repository.Save(counter).ConfigureAwait(false);
            messenger.Publish(new CountersChangedMessage(this));

            var props = new Dictionary<string, string>();
            props.Add("Counter Name", name);
            Analytics.TrackEvent("Add new counter", props);

            return counter;
        }

        public Task<List<Counter>> GetAllCounters()
        {
            return repository.GetAll();
        }

        public async Task DeleteCounter(Counter counter)
        {
            await repository.Delete(counter).ConfigureAwait(false);
            messenger.Publish(new CountersChangedMessage(this));
        }

        public Task IncrementCounter(Counter counter)
        {
            counter.Count += 1;
            return repository.Save(counter);
        }
    }
}