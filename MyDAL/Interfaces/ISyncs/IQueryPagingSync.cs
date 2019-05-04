using System;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IQueryPaging<M>
    where M : class
    {
        PagingResult<M> QueryPaging(int pageIndex, int pageSize);
        PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryPagingX
    {
        PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize)
            where M : class;
        PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }
}
