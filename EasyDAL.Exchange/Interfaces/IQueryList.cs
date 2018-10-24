using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryList<M>
        where M : class
    {
        Task<List<M>> QueryListAsync();
        Task<List<VM>> QueryListAsync<VM>()
            where VM : class;
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> func)
            where VM : class;
    }

    public interface IQueryListX
    {
        Task<List<M>> QueryListAsync<M>()
            where M : class;
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)
            where VM : class;
    }
}
