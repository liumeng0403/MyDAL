using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class CreateBatchImpl<M>
        : Impler
        , ICreateBatchAsync<M>, ICreateBatch<M>
        where M : class
    {
        internal CreateBatchImpl(Context dc)
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
                return await DC.DSA.ExecuteNonQueryAsync();
            });
        }

        public int CreateBatch(IEnumerable<M> mList)
        {
            DC.Action = ActionEnum.Insert;
            return DC.BDH.StepProcessSync(mList, 100, list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                PreExecuteHandle(UiMethodEnum.CreateBatchAsync);
                return DC.DSS.ExecuteNonQuery();
            });
        }
    }
}
