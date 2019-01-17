using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IPagingList<M>
        where M : class
    {
        Task<PagingResult<M>> PagingListAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);

    }

    internal interface IPagingListO<M>
        where M : class
    {
        Task<PagingResult<M>> PagingListAsync();
        Task<PagingResult<VM>> PagingListAsync<VM>()
            where VM : class;
        Task<PagingResult<T>> PagingListAsync<T>(Expression<Func<M, T>> columnMapFunc);

    }

    internal interface IPagingListX
    {
        Task<PagingResult<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingResult<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }

    internal interface IPagingListXO
    {
        Task<PagingResult<M>> PagingListAsync<M>()
            where M : class;
        Task<PagingResult<T>> PagingListAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
