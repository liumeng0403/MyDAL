using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryListAsync<M>
        where M : class
    {
        Task<List<M>> QueryListAsync(IDbTransaction tran = null);
        Task<List<VM>> QueryListAsync<VM>(IDbTransaction tran = null)
            where VM : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }
    internal interface IQueryList<M>
        where M : class
    {
        List<M> QueryList(IDbTransaction tran = null);
        List<VM> QueryList<VM>(IDbTransaction tran = null)
            where VM : class;
        List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListXAsync
    {
        Task<List<M>> QueryListAsync<M>(IDbTransaction tran = null)
            where M : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }
    internal interface IQueryListX
    {
        List<M> QueryList<M>(IDbTransaction tran = null)
            where M : class;
        List<T> QueryList<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListSQLAsync
    {
        Task<List<T>> QueryListAsync<T>(IDbTransaction tran = null);
    }
    internal interface IQueryListSQL
    {
        List<T> QueryList<T>(IDbTransaction tran = null);
    }
}
