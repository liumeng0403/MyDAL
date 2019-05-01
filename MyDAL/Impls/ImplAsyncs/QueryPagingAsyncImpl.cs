using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class QueryPagingAsyncImpl<M>
    : ImplerAsync
    , IQueryPagingAsync<M>
        where M : class
    {
        internal QueryPagingAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize, IDbTransaction tran = null)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderPagingAsync<None, M>(false, null);
        }
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderPagingAsync<M, VM>(false, null);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
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
            DSA.Tran = tran;
            return await DSA.ExecuteReaderPagingAsync<M, T>(single, columnMapFunc.Compile());
        }

    }

    internal sealed class QueryPagingXAsyncImpl
: ImplerAsync
, IQueryPagingXAsync
    {
        internal QueryPagingXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryPagingAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderPagingAsync<None, M>(false, null);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
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
            DSA.Tran = tran;
            return await DSA.ExecuteReaderPagingAsync<None, T>(single, null);
        }

    }
}
