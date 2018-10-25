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
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM : class;

        Task<List<M>> QueryListAsync(int topCount);
        Task<List<VM>> QueryListAsync<VM>(int topCount)
            where VM : class;
        Task<List<VM>> QueryListAsync<VM>(int topCount, Expression<Func<M, VM>> columnMapFunc)
            where VM : class;
    }

    public interface IQueryListX
    {
        Task<List<M>> QueryListAsync<M>()
            where M : class;
        Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM : class;

        Task<List<M>> QueryListAsync<M>(int topCount)
            where M : class;
        Task<List<VM>> QueryListAsync<VM>(int topCount, Expression<Func<VM>> columnMapFunc)
            where VM : class;
    }
}
