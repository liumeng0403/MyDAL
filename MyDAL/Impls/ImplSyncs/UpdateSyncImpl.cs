using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Data;

namespace MyDAL.Impls.ImplSyncs
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
