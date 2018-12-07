using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IPagingList<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> func);

    }

    internal interface IPagingListO<M>
        where M : class
    {
        Task<PagingList<M>> PagingListAsync(PagingQueryOption option);
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM : class;
        Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<M, T>> func);

    }

    internal interface IPagingListX
    {
        Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
            where VM : class;

    }

    internal interface IPagingListXO
    {
        Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class;
        Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
            where VM : class;

    }
}
