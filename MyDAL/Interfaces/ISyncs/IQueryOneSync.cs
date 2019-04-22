using System;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface IQueryOne<M>
    {
        M QueryOne(IDbTransaction tran = null);
        VM QueryOne<VM>(IDbTransaction tran = null)
            where VM : class;
        T QueryOne<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryOneX
    {
        M QueryOne<M>(IDbTransaction tran = null)
            where M : class;
        T QueryOne<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null);
    }

    internal interface IQueryOneSQL
    {
        T QueryOne<T>(IDbTransaction tran = null);
    }
}
