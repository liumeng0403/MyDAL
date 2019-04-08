using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface IQueryListAsync<M>
        where M : class
    {
        Task<List<M>> QueryListAsync();
        Task<List<VM>> QueryListAsync<VM>()
            where VM : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }
    internal interface IQueryList<M>
        where M : class
    {
        List<M> QueryList();
        List<VM> QueryList<VM>()
            where VM : class;
        List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryListXAsync
    {
        Task<List<M>> QueryListAsync<M>()
            where M : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryListX
    {
        List<M> QueryList<M>()
            where M : class;
        List<T> QueryList<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryListSQLAsync
    {
        Task<List<T>> QueryListAsync<T>();
    }
    internal interface IQueryListSQL
    {
        List<T> QueryList<T>();
    }
}
