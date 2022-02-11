using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ISelectList<M>
        where M : class
    {
        List<M> SelectList();
        List<VM> SelectList<VM>()
            where VM : class;
        List<T> SelectList<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectListX
    {
        List<M> SelectList<M>()
            where M : class;
        List<T> SelectList<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface ISelectListSQL
    {
        List<T> SelectList<T>();
    }
}
