using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class CreateBatchAsyncImpl<M>
    : ImplerAsync
    , ICreateBatchAsync<M>
    where M : class
    {
        internal CreateBatchAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> CreateBatchAsync(IEnumerable<M> mList, IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Insert;
            return await DC.BDH.StepProcess(mList, 100, async list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                PreExecuteHandle(UiMethodEnum.CreateBatchAsync);
                DSA.Tran = tran;
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
