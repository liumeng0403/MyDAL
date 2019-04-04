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
    internal interface IQueryOneSync<M>
    {
        M QueryOne();
        VM QueryOne<VM>()
            where VM : class;
        T QueryOne<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryOneX
    {
        Task<M> QueryOneAsync<M>()
            where M : class;
        Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryOneXSync
    {
        M QueryOne<M>()
            where M : class;
        T QueryOne<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryOneSQL
    {
        Task<T> QueryOneAsync<T>();
    }
    internal interface IQueryOneSQLSync
    {
        T QueryOne<T>();
    }
}
