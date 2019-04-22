using System;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ISum<M>
        where M : class
    {
        F Sum<F>(Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where F : struct;

        Nullable<F> Sum<F>(Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where F : struct;
    }
}
