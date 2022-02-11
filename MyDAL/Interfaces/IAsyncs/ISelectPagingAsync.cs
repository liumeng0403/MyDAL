using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ISelectPagingAsync<M>
        where M : class
    {
        Task<PagingResult<M>> SelectPagingAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> SelectPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> SelectPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectPagingXAsync
    {
        Task<PagingResult<M>> SelectPagingAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingResult<T>> SelectPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }
}
