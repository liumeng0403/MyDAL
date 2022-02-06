using System.Collections.Generic;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ICreateBatch<M>
    where M : class
    {
        int InsertBatch(IEnumerable<M> mList);
    }
}
