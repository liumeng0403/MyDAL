using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ISumAsync<M>
        where M : class
    {
        Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        Task<Nullable<F>> SumAsync<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct;
    }
    internal interface ISum<M>
        where M : class
    {
        F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        Nullable<F> Sum<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct;
    }
}
