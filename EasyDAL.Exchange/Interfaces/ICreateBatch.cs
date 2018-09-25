using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ICreateBatch<M>
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
}
