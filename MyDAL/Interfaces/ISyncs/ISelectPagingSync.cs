using System;
using System.Linq.Expressions;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface ISelectPaging<M>
    where M : class
    {
        PagingResult<M> SelectPaging(int pageIndex, int pageSize);
        PagingResult<VM> SelectPaging<VM>(int pageIndex, int pageSize)
            where VM : class;
        PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc);
    }

    internal interface ISelectPagingX
    {
        PagingResult<M> SelectPaging<M>(int pageIndex, int pageSize)
            where M : class;
        PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc);
    }
}
