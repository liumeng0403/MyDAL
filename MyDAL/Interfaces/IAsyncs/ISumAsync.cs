using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface ISumAsync<M>
        where M : class
    {
        Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        Task<Nullable<F>> SumAsync<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct;
    }
}
