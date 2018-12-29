using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryAll<M>
        where M : class
    {
        Task<List<M>> QueryAllAsync();
        Task<List<VM>> QueryAllAsync<VM>()
            where VM : class;
        Task<List<F>> QueryAllAsync<F>(Expression<Func<M, F>> propertyFunc);
    }

    internal interface IQueryAllX
    {
        Task<List<M>> QueryAllAsync<M>()
            where M : class;
        Task<List<T>> QueryAllAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
