using System;
using System.Linq.Expressions;

namespace MyDAL.Impls.Constraints.Methods
{
    internal interface ISum<M>
        where M : class
    {
        F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        F? Sum<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct;
    }

    internal interface ISumX
    {
        F Sum<F>(Expression<Func<F>> propertyFunc)
            where F : struct;

        F? Sum<F>(Expression<Func<F?>> propertyFunc)
            where F : struct;
    }
}
