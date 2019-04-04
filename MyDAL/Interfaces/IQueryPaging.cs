using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryPaging<M>
        where M : class
    {
        Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }
    internal interface IQueryPagingSync<M>
    where M : class
    {
        PagingResult<M> QueryPaging(int pageIndex, int pageSize);
        PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryPagingO<M>
        where M : class
    {
        Task<PagingResult<M>> QueryPagingAsync();
        Task<PagingResult<VM>> QueryPagingAsync<VM>()
            where VM : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }
    internal interface IQueryPagingOSync<M>
    where M : class
    {
        PagingResult<M> QueryPaging();
        PagingResult<VM> QueryPaging<VM>()
            where VM : class;
        PagingResult<T> QueryPaging<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryPagingX
    {
        Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryPagingXSync
    {
        PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize)
            where M : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryPagingXO
    {
        Task<PagingResult<M>> QueryPagingAsync<M>()
            where M : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryPagingXOSync
    {
        PagingResult<M> QueryPaging<M>()
            where M : class;
        PagingResult<T> QueryPaging<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryPagingSQL
    {
        Task<PagingResult<T>> QueryPagingAsync<T>();
    }
    internal interface IQueryPagingSQLSync
    {
        PagingResult<T> QueryPaging<T>();
    }
}
