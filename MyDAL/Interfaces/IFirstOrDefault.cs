using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IFirstOrDefault<M>
    {
        Task<M> FirstOrDefaultAsync();
        Task<VM> FirstOrDefaultAsync<VM>()
            where VM : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IFirstOrDefaultX
    {
        Task<M> FirstOrDefaultAsync<M>()
            where M : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T>> columnMapFunc);
    }
}
