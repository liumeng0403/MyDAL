using System.Collections.Generic;
using System.Data;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface ICreateBatch<M>
    where M : class
    {
        int CreateBatch(IEnumerable<M> mList);
    }
}
