using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class CreateBatchImpl<M>
        : Impler, ICreateBatch<M>
    {
        internal CreateBatchImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            return await DC.BDH.StepProcess(mList, 15, async list =>
            {
                DC.ResetConditions();
                DC.GetProperties(list);
                DC.IP.ConvertDic();
                return await SqlHelper.ExecuteAsync(
                    DC.Conn,
                    DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateBatchAsync)[0],
                    DC.SqlProvider.GetParameters());
            });
        }
    }
}
