using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Query
{
    public class SingleFilter<M>:Operator
    {

        internal SingleFilter(DbContext dc)
        {
            DC = dc;
        }

        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await SqlHelper.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QuerySingleValueAsync)[0],
                DC.SqlProvider.GetParameters());
        }

    }
}
