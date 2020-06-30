using System;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ICount<M>
        where M : class
    {
        int Count();
        int Count<F>(Expression<Func<M, F>> func);
    }

    internal interface ICountX
    {
        int Count();
        int Count<F>(Expression<Func<F>> func);
    }
}
