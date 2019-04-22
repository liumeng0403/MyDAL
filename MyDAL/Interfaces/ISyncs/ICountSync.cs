using System;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Interfaces.ISyncs
{
    internal interface ICount<M>
        where M : class
    {
        int Count(IDbTransaction tran = null);
        int Count<F>(Expression<Func<M, F>> func, IDbTransaction tran = null);
    }

    internal interface ICountX
    {
        int Count(IDbTransaction tran = null);
        int Count<F>(Expression<Func<F>> func, IDbTransaction tran = null);
    }
}
