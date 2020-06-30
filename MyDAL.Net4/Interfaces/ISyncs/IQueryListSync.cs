using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IQueryList<M>
        where M : class
    {
        List<M> QueryList();
        List<VM> QueryList<VM>()
            where VM : class;
        List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryListX
    {
        List<M> QueryList<M>()
            where M : class;
        List<T> QueryList<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryListSQL
    {
        List<T> QueryList<T>();
    }
}
