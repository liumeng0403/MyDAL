using System.Collections.Generic;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IInsertBatch<M>
    where M : class
    {
        int InsertBatch(IEnumerable<M> mList);
    }
}
