using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IPagingList<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);

    }

    internal interface IPagingListO<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(PagingQueryOption option);
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM : class;
        Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<M, T>> columnMapFunc);

    }

    internal interface IPagingListX
    {
        Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }

    internal interface IPagingListXO
    {
        Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class;
        Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<T>> columnMapFunc);
    }
}
