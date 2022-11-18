using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System.Collections.Generic;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class InsertBatchImpl<M>
        : ImplerBase
        , IInsertBatch<M>
        where M : class
    {
        internal InsertBatchImpl(Context dc)
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
