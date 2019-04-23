using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class QueryPagingImpl<M>
        : ImplerSync
        , IQueryPaging<M>
            where M : class
    {
        internal QueryPagingImpl(Context dc)
            : base(dc)
        { }

        public PagingResult<M> QueryPaging(int pageIndex, int pageSize, IDbTransaction tran = null)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<None, M>(false, null);
        }
        public PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<M, VM>(false, null);
        }
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
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
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<M, T>(single, columnMapFunc.Compile());
        }
    }

    internal sealed class QueryPagingXImpl
        : ImplerSync
        , IQueryPagingX
    {
        internal QueryPagingXImpl(Context dc)
            : base(dc)
        { }

        public PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<None, M>(false, null);
        }
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
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
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<None, T>(single, null);
        }
    }

    internal sealed class QueryPagingSQLImpl
        : ImplerSync
        , IQueryPagingSQL
    {
        public QueryPagingSQLImpl(Context dc)
            : base(dc)
        { }

        public PagingResult<T> QueryPaging<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryPagingAsync;
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<None, T>(typeof(T).IsSingleColumn(), null);
        }
    }
}
