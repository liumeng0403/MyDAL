using System.Collections.Generic;

namespace MyDAL.Impls.Constraints.Methods
{
    internal interface IInsertBatch<M>
    where M : class
    {
        int InsertBatch(IEnumerable<M> mList);
    }
}
