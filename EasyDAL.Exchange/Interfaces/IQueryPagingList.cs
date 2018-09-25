using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IQueryPagingList<M>
    {
        Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func);

        Task<PagingList<M>> QueryPagingListAsync(PagingQueryOption option);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func);
    }

    internal interface IQueryPagingListX
    {
        Task<PagingList<M>> QueryPagingListAsync<M>(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func);

        Task<PagingList<M>> QueryPagingListAsync<M>(PagingQueryOption option);
        Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func);
    }
}
