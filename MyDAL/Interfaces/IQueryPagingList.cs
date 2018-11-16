using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryPagingList<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
            where VM : class;

    }

    internal interface IQueryPagingListO<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(PagingQueryOption option);
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM : class;
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
            where VM : class;

    }

    internal interface IQueryPagingListX
    {
        Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
            where VM : class;

    }

    internal interface IQueryPagingListXO
    {
        Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class;
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
            where VM : class;

    }
}
