namespace Yunyong.DataExchange.Core.Enums
{
    internal enum UiMethodEnum
    {
        None,
        CreateAsync,
        CreateBatchAsync,
        DeleteAsync,
        UpdateAsync,
        QueryFirstOrDefaultAsync,
        JoinQueryFirstOrDefaultAsync,
        QueryListAsync,
        TopAsync,
        JoinQueryListAsync,
        JoinTopAsync,
        QueryPagingListAsync,
        JoinQueryPagingListAsync,
        QueryAllAsync,
        QueryAllPagingListAsync,
        QuerySingleValueAsync,
        ExistAsync,
        CountAsync,
        JoinCountAsync
    }
}
