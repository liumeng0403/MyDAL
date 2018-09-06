using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Create
{
    public class Creater<M>: Operator
    {
        internal Creater(DbContext dc)
        {
            DC = dc;
        }


        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateAsync(M m)
        {
            await DC.GetProperties(m);
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.CreateAsync)[0],
                DC.GetParameters());
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateBatchAsync(IEnumerable<M> mList)
        {
            await DC.GetProperties(mList);
            return await SqlHelper.ExecuteAsync(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.CreateBatchAsync)[0],
                DC.GetParameters());
        }

    }
}
