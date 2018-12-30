using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryList<M>
        where M : class
    {
        Task<List<M>> QueryListAsync();
        Task<List<VM>> QueryListAsync<VM>()
            where VM : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryListX
    {
        Task<List<M>> QueryListAsync<M>()
            where M : class;
        Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
