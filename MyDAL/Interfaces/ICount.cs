using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICountAsync<M>
        where M:class
    {
        Task<int> CountAsync();
        Task<int> CountAsync<F>(Expression<Func<M, F>> func);
    }
    internal interface ICount<M>
        where M : class
    {
        int Count();
        int Count<F>(Expression<Func<M, F>> func);
    }

    internal interface ICountXAsync
    {
        Task<int> CountAsync();
        Task<int> CountAsync<F>(Expression<Func<F>> func);
    }
    internal interface ICountX
    {
        int Count();
        int Count<F>(Expression<Func<F>> func);
    }
}
