using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface IQueryOneAsync<M>
    {
        Task<M> QueryOneAsync();
        Task<VM> QueryOneAsync<VM>()
            where VM : class;
        Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }
    internal interface IQueryOne<M>
    {
        M QueryOne();
        VM QueryOne<VM>()
            where VM : class;
        T QueryOne<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryOneXAsync
    {
        Task<M> QueryOneAsync<M>()
            where M : class;
        Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc);
    }
    internal interface IQueryOneX
    {
        M QueryOne<M>()
            where M : class;
        T QueryOne<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryOneSQLAsync
    {
        Task<T> QueryOneAsync<T>();
    }
    internal interface IQueryOneSQL
    {
        T QueryOne<T>();
    }
}
