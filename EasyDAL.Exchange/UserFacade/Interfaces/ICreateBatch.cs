using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Interfaces
{
    internal interface ICreateBatch<M>
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList);
    }
}
