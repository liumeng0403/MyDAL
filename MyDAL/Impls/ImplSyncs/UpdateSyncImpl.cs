using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Data;

namespace HPC.DAL.Impls.ImplSyncs
{
    internal sealed class UpdateImpl<M>
        : ImplerSync
        , IUpdate<M>
        where M : class
    {
        internal UpdateImpl(Context dc)
            : base(dc)
        { }

        public int Update()
        {
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return DSS.ExecuteNonQuery();
        }
    }
}
