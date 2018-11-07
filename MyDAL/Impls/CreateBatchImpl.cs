using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class CreateBatchImpl<M>
        : Impler, ICreateBatch<M>
        where M:class
    {
        internal CreateBatchImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            DC.Action = ActionEnum.Insert;
            return await DC.BDH.StepProcess(mList, 35, async list =>
            {
                DC.DPH.ResetParameter();
                CreateMHandle(list);
                DC.DPH.SetParameter();
                DC.Method = UiMethodEnum.CreateBatchAsync;
                DC.SqlProvider.GetSQL<M>();
                return await DC.DS.ExecuteNonQueryAsync();
            });
        }
    }
}
