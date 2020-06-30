using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IQueryOneAsync<M>
    {
        Task<M> QueryOneAsync();
        Task<VM> QueryOneAsync<VM>()
            where VM : class;
        Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryOneXAsync
    {
        Task<M> QueryOneAsync<M>()
            where M : class;
        Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryOneSQLAsync
    {
        Task<T> QueryOneAsync<T>();
    }
}
