using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.UserFacade.Interfaces;

namespace EasyDAL.Exchange.UserFacade.Create
{
    public class Creater<M> 
        : Operator, IMethodObject, ICreate<M>, ICreateBatch<M>
    {
        internal Creater(Context dc) 
            : base(dc)
        { }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateAsync(M m)
        {
            DC.GetProperties(m);
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CreateAsync)[0],
                DC.GetParameters());
        }
        
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
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
