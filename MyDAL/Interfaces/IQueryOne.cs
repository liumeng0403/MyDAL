using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryOne<M>
    {
        Task<M> QueryOneAsync();
        Task<VM> QueryOneAsync<VM>()
            where VM : class;
        Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryOneX
    {
        Task<M> QueryOneAsync<M>()
            where M : class;
        Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryOneSQL
    {
        Task<T> QueryOneAsync<T>();
    }
}
