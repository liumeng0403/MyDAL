using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IPagingAll<M>
    {
        Task<PagingResult<M>> PagingAllAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> PagingAllAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> PagingAllAsync<T>(int pageIndex, int pageSize,Expression<Func<M, T>> columnMapFunc);
    }
}
