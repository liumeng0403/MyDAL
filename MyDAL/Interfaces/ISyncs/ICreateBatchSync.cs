using System.Collections.Generic;
using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ICreateBatch<M>
    where M : class
    {
        int CreateBatch(IEnumerable<M> mList, IDbTransaction tran = null);
    }
}
