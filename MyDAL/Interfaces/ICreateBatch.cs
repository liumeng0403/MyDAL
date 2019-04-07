using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreateBatchAsync<M>
        where M:class
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
    internal interface ICreateBatch<M>
    where M : class
    {
        int CreateBatch(IEnumerable<M> mList);
    }
}
