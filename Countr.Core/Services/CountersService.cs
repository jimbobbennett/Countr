using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
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
            await repository.Delete(counter);
            messenger.Publish(new CountersChangedMessage(this));
        }

        public async Task IncrementCounter(Counter counter)
        {
            counter.Count += 1;
            await repository.Save(counter);
        }
    }
}