using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ICreateBatchAsync<M>
        where M : class
    {
        Task<int> InsertBatchAsync(IEnumerable<M> mList);
    }
}
