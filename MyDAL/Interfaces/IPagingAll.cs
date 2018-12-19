using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IPagingAll<M>
    {
        Task<PagingList<M>> PagingAllAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingAllAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingList<T>> PagingAllAsync<T>(int pageIndex, int pageSize,Expression<Func<M, T>> columnMapFunc);
    }
}
