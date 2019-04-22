using System;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IQueryPaging<M>
    where M : class
    {
        PagingResult<M> QueryPaging(int pageIndex, int pageSize, IDbTransaction tran = null);
        PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where VM : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryPagingX
    {
        PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryPagingSQL
    {
        PagingResult<T> QueryPaging<T>(IDbTransaction tran = null);
    }
}
