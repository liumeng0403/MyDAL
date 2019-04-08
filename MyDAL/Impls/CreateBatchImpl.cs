using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPC.DAL.Impls
{
    internal sealed class CreateBatchAsyncImpl<M>
    : ImplerAsync
    , ICreateBatchAsync<M> 
    where M : class
    {
        internal CreateBatchAsyncImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            DC.Action = ActionEnum.Insert;
            return await DC.BDH.StepProcess(mList, 100, async list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                PreExecuteHandle(UiMethodEnum.CreateBatchAsync);
                return await DSA.ExecuteNonQueryAsync();
            });
        }
 
    }
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
                PreExecuteHandle(UiMethodEnum.CreateBatchAsync);
                return DSS.ExecuteNonQuery();
            });
        }
    }
}
