using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IQueryFirstOrDefault<M>
        where M : class
    {
        Task<M> QueryFirstOrDefaultAsync();
        Task<VM> QueryFirstOrDefaultAsync<VM>()
            where VM : class;
        Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func)
            where VM : class;
    }

    internal interface IQueryFirstOrDefaultX
    {
        Task<M> QueryFirstOrDefaultAsync<M>()
            where M : class;
        Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func)
            where VM : class;
    }
}
