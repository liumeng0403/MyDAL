using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;

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

        public int InsertBatch(IEnumerable<M> mList)
        {
            DC.Action = ActionEnum.Insert;
            var tm = DC.XC.GetTableModel(typeof(M));
            if (tm.HaveAutoIncrementPK)
            {
                return DC.BDH.StepProcessSync(mList, 1, list =>
                {
                    DC.DPH.ResetParameter();
                    CreateMHandle(list);
                    PreExecuteHandle(UiMethodEnum.CreateBatch);
                    return DSS.ExecuteNonQuery<M>(list);
                });
            }
            else
            {
                return DC.BDH.StepProcessSync(mList, 100, list =>
                {
                    DC.DPH.ResetParameter();
                    CreateMHandle(list);
                    PreExecuteHandle(UiMethodEnum.CreateBatch);
                    return DSS.ExecuteNonQuery<M>(list);
                });
            }
        }
    }
}
