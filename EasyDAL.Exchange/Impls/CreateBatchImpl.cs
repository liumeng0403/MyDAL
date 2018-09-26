using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using EasyDAL.Exchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
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
                return await SqlHelper.ExecuteAsync(
                    DC.Conn,
                    DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateBatchAsync)[0],
                    DC.GetParameters());
            });
        }
    }
}
