using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IList<M>
        where M : class
    {
        Task<List<M>> ListAsync();
        Task<List<VM>> ListAsync<VM>()
            where VM : class;
        Task<List<T>> ListAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IListX
    {
        Task<List<M>> ListAsync<M>()
            where M : class;
        Task<List<T>> ListAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
