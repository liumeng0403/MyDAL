using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IQueryListAsync<M>
        where M : class
    {
        Task<List<M>> QueryListAsync(IDbTransaction tran = null);
        Task<List<VM>> QueryListAsync<VM>(IDbTransaction tran = null)
            where VM : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListXAsync
    {
        Task<List<M>> QueryListAsync<M>(IDbTransaction tran = null)
            where M : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListSQLAsync
    {
        Task<List<T>> QueryListAsync<T>(IDbTransaction tran = null);
    }
}
