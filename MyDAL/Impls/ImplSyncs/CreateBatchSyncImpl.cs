using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class CreateBatchImpl<M>
        : ImplerSync
        , ICreateBatch<M>
        where M : class
    {
        internal CreateBatchImpl(Context dc)
            : base(dc)
        {
        }

        public int CreateBatch(IEnumerable<M> mList, IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Insert;
            return DC.BDH.StepProcessSync(mList, 100, list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                PreExecuteHandle(UiMethodEnum.CreateBatchAsync);
                DSS.Tran = tran;
                return DSS.ExecuteNonQuery();
            });
        }
    }
}
