using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;

namespace HPC.DAL.Impls.ImplSyncs
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

        public int CreateBatch(IEnumerable<M> mList)
        {
            DC.Action = ActionEnum.Insert;
            return DC.BDH.StepProcessSync(mList, 100, list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                PreExecuteHandle(UiMethodEnum.CreateBatch);
                return DSS.ExecuteNonQuery();
            });
        }
    }
}
