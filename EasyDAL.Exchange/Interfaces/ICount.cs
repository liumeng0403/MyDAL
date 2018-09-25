using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ICount<M>
    {
        Task<long> CountAsync();
        Task<long> CountAsync<F>(Expression<Func<M, F>> func);
    }
}
