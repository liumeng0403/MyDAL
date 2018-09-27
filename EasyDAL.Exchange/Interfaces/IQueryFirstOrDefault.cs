using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryFirstOrDefault<M>
    {
        Task<M> QueryFirstOrDefaultAsync();
        Task<VM> QueryFirstOrDefaultAsync<VM>();
        Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func);
    }

    internal interface IQueryFirstOrDefaultX
    {
        Task<M> QueryFirstOrDefaultAsync<M>();
        Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func);
    }
}
