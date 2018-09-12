using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;

namespace Yunyong.DataExchange.UserFacade.Query
{
    public class SingleFilter<M>:Operator,IMethodObject
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
