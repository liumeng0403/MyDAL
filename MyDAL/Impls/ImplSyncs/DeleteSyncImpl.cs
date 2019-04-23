using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Data;

namespace HPC.DAL.Impls.ImplSyncs
{
    internal sealed class DeleteImpl<M>
        : ImplerSync
        , IDelete
        where M : class
    {
        internal DeleteImpl(Context dc)
            : base(dc)
        { }

        public int Delete(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            DSS.Tran = tran;
            return DSS.ExecuteNonQuery();
        }

    }
}
