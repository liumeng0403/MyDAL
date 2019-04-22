using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface IQueryList<M>
        where M : class
    {
        List<M> QueryList(IDbTransaction tran = null);
        List<VM> QueryList<VM>(IDbTransaction tran = null)
            where VM : class;
        List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListX
    {
        List<M> QueryList<M>(IDbTransaction tran = null)
            where M : class;
        List<T> QueryList<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryListSQL
    {
        List<T> QueryList<T>(IDbTransaction tran = null);
    }
}
