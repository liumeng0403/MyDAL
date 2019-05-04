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

        public int Delete()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return DSS.ExecuteNonQuery();
        }

    }
}
