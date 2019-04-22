using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface ICountAsync<M>
        where M : class
    {
        Task<int> CountAsync(IDbTransaction tran = null);
        Task<int> CountAsync<F>(Expression<Func<M, F>> func, IDbTransaction tran = null);
    }

    internal interface ICountXAsync
    {
        Task<int> CountAsync(IDbTransaction tran = null);
        Task<int> CountAsync<F>(Expression<Func<F>> func, IDbTransaction tran = null);
    }
}
