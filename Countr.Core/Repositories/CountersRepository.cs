using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Countr.Core.Models;
using PCLStorage;
using SQLite;

namespace Countr.Core.Repositories
{
    public class CountersRepository : ICountersRepository
    {
        readonly SQLiteAsyncConnection connection;

        public CountersRepository()
        {
            var local = FileSystem.Current.LocalStorage.Path;
            var datafile = Path.Combine(local, "counters.db3");
            connection = new SQLiteAsyncConnection(datafile);
            connection.CreateTableAsync<Counter>().Wait();
        }

        public async Task Save(Counter counter)
        {
            await connection.InsertOrReplaceAsync(counter);
        }

        public async Task<IEnumerable<Counter>> GetAll()
        {
            return await connection.Table<Counter>().ToListAsync();
        }

        public async Task Delete(Counter counter)
        {
            await connection.DeleteAsync(counter);
        }
    }
}
