using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Data;

namespace MyDAL.Impls.ImplSyncs
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
