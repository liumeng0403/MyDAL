using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Enums
{
    internal enum SqlTypeEnum
    {
        CreateAsync,
        DeleteAsync,
        UpdateAsync,
        QueryFirstOrDefaultAsync,
        QueryListAsync,
        JoinQueryListAsync,
        QueryPagingListAsync,
        QuerySingleValueAsync,
        ExistAsync,
        QueryAllAsync
    }
}
