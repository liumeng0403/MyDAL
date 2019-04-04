using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICount<M>
        where M:class
    {
        Task<int> CountAsync();
        Task<int> CountAsync<F>(Expression<Func<M, F>> func);
    }
    internal interface ICountSync<M>
        where M : class
    {
        int Count();
        int Count<F>(Expression<Func<M, F>> func);
    }

    internal interface ICountX
    {
        Task<int> CountAsync();
        Task<int> CountAsync<F>(Expression<Func<F>> func);
    }
    internal interface ICountXSync
    {
        int Count();
        int Count<F>(Expression<Func<F>> func);
    }
}
