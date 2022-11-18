using System;
using System.Linq.Expressions;

namespace MyDAL.Impls.Constraints.Methods
{
    internal interface ISelectOne<M>
    {
        M SelectOne();
        VM SelectOne<VM>()
            where VM : class;
        T SelectOne<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectOneX
    {
        M SelectOne<M>()
            where M : class;
        T SelectOne<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface ISelectOneSQL
    {
        T SelectOne<T>();
    }
}
