using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ISelectOneAsync<M>
    {
        Task<M> SelectOneAsync();
        Task<VM> SelectOneAsync<VM>()
            where VM : class;
        Task<T> SelectOneAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectOneXAsync
    {
        Task<M> SelectOneAsync<M>()
            where M : class;
        Task<T> SelectOneAsync<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface ISelectOneSQLAsync
    {
        Task<T> SelectOneAsync<T>();
    }
}
