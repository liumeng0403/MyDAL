using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Query
{
    public class SingleFilter<M>:Operator
    {

        internal SingleFilter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// 单表单值查询
        /// </summary>
        /// <typeparam name="V">int/long/decimal/...</typeparam>
        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await SqlHelper.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QuerySingleValueAsync)[0],
                DC.GetParameters());
        }

    }
}
