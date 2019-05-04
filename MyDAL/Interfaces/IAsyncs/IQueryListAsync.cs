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
        Task<List<M>> QueryListAsync();
        Task<List<VM>> QueryListAsync<VM>()
            where VM : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryListXAsync
    {
        Task<List<M>> QueryListAsync<M>()
            where M : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryListSQLAsync
    {
        Task<List<T>> QueryListAsync<T>();
    }
}
