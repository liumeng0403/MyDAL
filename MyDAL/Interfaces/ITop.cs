using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ITop<M>
        where M : class
    {
        Task<List<M>> TopAsync(int count);
        Task<List<VM>> TopAsync<VM>(int count)
            where VM : class;
        Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ITopX
    {
        Task<List<M>> TopAsync<M>(int count)
            where M : class;
        Task<List<VM>> TopAsync<VM>(int count,Expression<Func<VM>> columnMapFunc)
            where VM : class;
    }
}
