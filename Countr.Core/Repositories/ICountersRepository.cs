using Countr.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Countr.Core.Repositories
{
    public interface ICountersRepository
    {
        Task Save(Counter counter);
        Task<IEnumerable<Counter>> GetAll();
        Task Delete(Counter counter);
    }
}
