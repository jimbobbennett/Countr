using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;

namespace Countr.Core.Repositories
{
    public interface ICountersRepository
    {
        Task Save(Counter counter);
        Task<List<Counter>> GetAll();
        Task Delete(Counter counter);
    }
}