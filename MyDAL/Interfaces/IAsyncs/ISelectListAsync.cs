using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ISelectListAsync<M>
        where M : class
    {
        Task<List<M>> SelectListAsync();
        Task<List<VM>> SelectListAsync<VM>()
            where VM : class;
        Task<List<T>> SelectListAsync<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectListXAsync
    {
        Task<List<M>> SelectListAsync<M>()
            where M : class;
        Task<List<T>> SelectListAsync<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface ISelectListSQLAsync
    {
        Task<List<T>> SelectListAsync<T>();
    }
}
