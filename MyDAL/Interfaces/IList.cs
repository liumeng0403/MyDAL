using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IList<M>
        where M : class
    {
        Task<List<M>> ListAsync();
        Task<List<VM>> ListAsync<VM>()
            where VM : class;
        Task<List<T>> ListAsync<T>(Expression<Func<M, T>> columnMapFunc);

        Task<List<M>> ListAsync(int topCount);
        Task<List<VM>> ListAsync<VM>(int topCount)
            where VM : class;
        Task<List<T>> ListAsync<T>(int topCount, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IListX
    {
        Task<List<M>> ListAsync<M>()
            where M : class;
        Task<List<VM>> ListAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM : class;

        Task<List<M>> ListAsync<M>(int topCount)
            where M : class;
        Task<List<VM>> ListAsync<VM>(int topCount, Expression<Func<VM>> columnMapFunc)
            where VM : class;
    }
}
