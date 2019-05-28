using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
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
                PreExecuteHandle(UiMethodEnum.CreateBatch);
                return await DSA.ExecuteNonQueryAsync();
            });
        }

    }
}
