using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyDAL.Impls.Constraints.Methods
{
    internal interface ITop<M>
        where M : class
    {
        List<M> Top(int count);
        List<VM> Top<VM>(int count)
            where VM : class;
        List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ITopX
    {
        List<M> Top<M>(int count)
            where M : class;
        List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc);
    }
}
