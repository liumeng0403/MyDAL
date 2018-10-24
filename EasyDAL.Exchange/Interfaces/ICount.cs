using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ICount<M>
        where M:class
    {
        Task<long> CountAsync();
        Task<long> CountAsync<F>(Expression<Func<M, F>> func);
    }

    internal interface ICountX
    {
        Task<long> CountAsync();
        Task<long> CountAsync<F>(Expression<Func<F>> func);
    }
}
