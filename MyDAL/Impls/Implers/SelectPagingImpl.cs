using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class SelectPagingImpl<M>
        : ImplerBase
        , ISelectPaging<M>
            where M : class
    {
        internal SelectPagingImpl(Context dc)
            : base(dc)
        { }

        public PagingResult<M> SelectPaging(int pageIndex, int pageSize)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPaging);
            return DSS.ExecuteReaderPaging<None, M>(false, null);
        }
        public PagingResult<VM> SelectPaging<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPaging);
            return DSS.ExecuteReaderPaging<M, VM>(false, null);
        }
        public PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            PreExecuteHandle(UiMethodEnum.QueryPaging);
            return DSS.ExecuteReaderPaging<M, T>(single, columnMapFunc.Compile());
        }
    }

    internal sealed class SelectPagingXImpl
        : ImplerBase
        , ISelectPagingX
    {
        internal SelectPagingXImpl(Context dc)
            : base(dc)
        { }

        public PagingResult<M> SelectPaging<M>(int pageIndex, int pageSize)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryPaging);
            return DSS.ExecuteReaderPaging<None, M>(false, null);
        }
        public PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            PreExecuteHandle(UiMethodEnum.QueryPaging);
            return DSS.ExecuteReaderPaging<None, T>(single, null);
        }
    }
}
