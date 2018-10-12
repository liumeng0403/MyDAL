using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
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
            return await DC.BDH.StepProcess(mList, 35, async list =>
            {
                DC.ResetConditions();
                DC.GetProperties(list);
                DC.IP.ConvertDic();
                return await DC.DS.ExecuteNonQueryAsync(
                    DC.Conn,
                    DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateBatchAsync)[0],
                    DC.SqlProvider.GetParameters());
            });
        }
    }
}
