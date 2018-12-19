using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IAll<M>
        where M : class
    {
        Task<List<M>> AllAsync();
        Task<List<VM>> AllAsync<VM>()
            where VM : class;
        Task<List<F>> AllAsync<F>(Expression<Func<M, F>> propertyFunc);
    }

    internal interface IAllX
    {
        Task<List<M>> AllAsync<M>()
            where M : class;
        Task<List<T>> AllAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
