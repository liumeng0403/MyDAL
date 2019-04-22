using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface ITop<M>
        where M : class
    {
        List<M> Top(int count, IDbTransaction tran = null);
        List<VM> Top<VM>(int count, IDbTransaction tran = null)
            where VM : class;
        List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface ITopX
    {
        List<M> Top<M>(int count, IDbTransaction tran = null)
            where M : class;
        List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }
}
