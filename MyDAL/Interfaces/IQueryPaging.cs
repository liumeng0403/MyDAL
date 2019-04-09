using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryPagingAsync<M>
        where M : class
    {
        Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }
    internal interface IQueryPaging<M>
    where M : class
    {
        PagingResult<M> QueryPaging(int pageIndex, int pageSize);
        PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryPagingXAsync
    {
        Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryPagingX
    {
        PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize)
            where M : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryPagingSQLAsync
    {
        Task<PagingResult<T>> QueryPagingAsync<T>();
    }
    internal interface IQueryPagingSQL
    {
        PagingResult<T> QueryPaging<T>();
    }
}
