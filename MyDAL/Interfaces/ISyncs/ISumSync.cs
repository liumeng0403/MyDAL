using System;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface ISum<M>
        where M : class
    {
        F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct;

        Nullable<F> Sum<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct;
    }
}
