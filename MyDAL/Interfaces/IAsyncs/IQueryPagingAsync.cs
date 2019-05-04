using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IQueryPagingAsync<M>
        where M : class
    {
        Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize);
        Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryPagingXAsync
    {
        Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class;
        Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryPagingSQLAsync
    {
        Task<PagingResult<T>> QueryPagingAsync<T>();
    }
}
