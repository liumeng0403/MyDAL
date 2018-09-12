using System;
using System.Collections.Generic;
using System.Text;

namespace Yunyong.DataExchange.Enums
{
    internal enum SqlTypeEnum
    {
        CreateAsync,
        CreateBatchAsync,
        DeleteAsync,
        UpdateAsync,
        QueryFirstOrDefaultAsync,
        QueryListAsync,
        JoinQueryListAsync,
        QueryPagingListAsync,
        QueryAllPagingListAsync,
        QuerySingleValueAsync,
        ExistAsync,
        QueryAllAsync
    }
}
