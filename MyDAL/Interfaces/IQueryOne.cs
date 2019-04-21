using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryOneAsync<M>
    {
        Task<M> QueryOneAsync(IDbTransaction tran = null);
        Task<VM> QueryOneAsync<VM>(IDbTransaction tran = null)
            where VM : class;
        Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc,IDbTransaction tran = null);
    }
    internal interface IQueryOne<M>
    {
        M QueryOne(IDbTransaction tran = null);
        VM QueryOne<VM>(IDbTransaction tran = null)
            where VM : class;
        T QueryOne<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryOneXAsync
    {
        Task<M> QueryOneAsync<M>(IDbTransaction tran = null)
            where M : class;
        Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }
    internal interface IQueryOneX
    {
        M QueryOne<M>(IDbTransaction tran = null)
            where M : class;
        T QueryOne<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryOneSQLAsync
    {
        Task<T> QueryOneAsync<T>(IDbTransaction tran = null);
    }
    internal interface IQueryOneSQL
    {
        T QueryOne<T>(IDbTransaction tran = null);
    }
}
