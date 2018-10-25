using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ITop<M>
        where M : class
    {
        Task<List<M>> TopAsync(int count);
        Task<List<VM>> TopAsync<VM>(int count)
            where VM : class;
        Task<List<VM>> TopAsync<VM>(int count, Expression<Func<M, VM>> columnMapFunc)
            where VM : class;
    }
}
