using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IQueryPagingAsync<M>
        where M : class
    {
        Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize, IDbTransaction tran = null);
        Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where VM : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryPagingXAsync
    {
        Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryPagingSQLAsync
    {
        Task<PagingResult<T>> QueryPagingAsync<T>(IDbTransaction tran = null);
    }
}
