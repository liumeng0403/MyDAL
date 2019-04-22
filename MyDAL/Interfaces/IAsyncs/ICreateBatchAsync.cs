using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ICreateBatchAsync<M>
        where M : class
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList, IDbTransaction tran = null);
    }
}
