using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ITopAsync<M>
        where M : class
    {
        Task<List<M>> TopAsync(int count, IDbTransaction tran = null);
        Task<List<VM>> TopAsync<VM>(int count, IDbTransaction tran = null)
            where VM : class;
        Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }
    internal interface ITop<M>
        where M : class
    {
        List<M> Top(int count, IDbTransaction tran = null);
        List<VM> Top<VM>(int count, IDbTransaction tran = null)
            where VM : class;
        List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface ITopXAsync
    {
        Task<List<M>> TopAsync<M>(int count, IDbTransaction tran = null)
            where M : class;
        Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }
    internal interface ITopX
    {
        List<M> Top<M>(int count, IDbTransaction tran = null)
            where M : class;
        List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }
}
