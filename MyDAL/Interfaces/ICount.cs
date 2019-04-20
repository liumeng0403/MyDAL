using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface ICountAsync<M>
        where M : class
    {
        Task<int> CountAsync(IDbTransaction tran = null);
        Task<int> CountAsync<F>(Expression<Func<M, F>> func, IDbTransaction tran = null);
    }
    internal interface ICount<M>
        where M : class
    {
        int Count(IDbTransaction tran = null);
        int Count<F>(Expression<Func<M, F>> func, IDbTransaction tran = null);
    }

    internal interface ICountXAsync
    {
        Task<int> CountAsync(IDbTransaction tran = null);
        Task<int> CountAsync<F>(Expression<Func<F>> func, IDbTransaction tran = null);
    }
    internal interface ICountX
    {
        int Count(IDbTransaction tran = null);
        int Count<F>(Expression<Func<F>> func, IDbTransaction tran = null);
    }
}
