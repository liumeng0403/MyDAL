using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.UserFacade.Interfaces
{
    internal interface ICreateBatch<M>
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
}
