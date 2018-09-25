using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface ICreateBatch<M>
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
}
