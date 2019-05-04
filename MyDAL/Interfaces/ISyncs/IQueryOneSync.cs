using System;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface IQueryOne<M>
    {
        M QueryOne();
        VM QueryOne<VM>()
            where VM : class;
        T QueryOne<T>(Expression<Func<M, T>> columnMapFunc);
    }

    internal interface IQueryOneX
    {
        M QueryOne<M>()
            where M : class;
        T QueryOne<T>(Expression<Func<T>> columnMapFunc);
    }

    internal interface IQueryOneSQL
    {
        T QueryOne<T>();
    }
}
