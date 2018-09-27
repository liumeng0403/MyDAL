using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreateBatch<M>
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
}
