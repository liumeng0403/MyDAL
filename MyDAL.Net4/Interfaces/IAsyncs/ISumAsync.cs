using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ISumAsync<M>
        where M : class
    {
        Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        Task<F?> SumAsync<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct;
    }

    internal interface ISumXAsync
    {
        Task<F> SumAsync<F>(Expression<Func<F>> propertyFunc)
            where F : struct;

        Task<F?> SumAsync<F>(Expression<Func<F?>> propertyFunc)
            where F : struct;
    }
}
