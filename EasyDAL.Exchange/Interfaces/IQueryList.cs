using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IQueryList<M>
    {
        Task<List<M>> QueryListAsync();
        Task<List<VM>> QueryListAsync<VM>();
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> func);
    }

    public interface IQueryListX
    {
        Task<List<M>> QueryListAsync<M>();
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func);
    }
}
