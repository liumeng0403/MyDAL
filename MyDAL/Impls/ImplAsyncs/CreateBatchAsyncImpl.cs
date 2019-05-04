using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class CreateBatchAsyncImpl<M>
    : ImplerAsync
    , ICreateBatchAsync<M>
    where M : class
    {
        internal CreateBatchAsyncImpl(Context dc)
            : base(dc)
        { }

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
}
