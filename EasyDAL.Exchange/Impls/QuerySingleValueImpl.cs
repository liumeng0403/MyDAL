using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QuerySingleValueImpl<M>
        : Impler, IQuerySingleValue<M>
    {
        internal QuerySingleValueImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await SqlHelper.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QuerySingleValueAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
