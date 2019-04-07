using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class QueryPagingImpl<M>
        : Impler
        , IQueryPagingAsync<M>, IQueryPaging<M>
            where M : class
    {
        internal QueryPagingImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.QueryPagingAsync, false, null);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
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
            return await PagingListAsyncHandle<M, T>(UiMethodEnum.QueryPagingAsync, single, columnMapFunc.Compile());
        }

        public PagingResult<M> QueryPaging(int pageIndex, int pageSize)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return PagingListAsyncHandleSync<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return PagingListAsyncHandleSync<M, VM>(UiMethodEnum.QueryPagingAsync, false, null);
        }
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
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
            return PagingListAsyncHandleSync<M, T>(UiMethodEnum.QueryPagingAsync, single, columnMapFunc.Compile());
        }
    }

    internal sealed class QueryPagingOImpl<M>
        : Impler
        , IQueryPagingOAsync<M>, IQueryPagingO<M>
            where M : class
    {
        internal QueryPagingOImpl(Context dc)
            : base(dc) { }

        public async Task<PagingResult<M>> QueryPagingAsync()
        {
            return await PagingListAsyncHandle<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.QueryPagingAsync, false, null);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle(UiMethodEnum.QueryPagingAsync, single, columnMapFunc.Compile());
        }

        public PagingResult<M> QueryPaging()
        {
            return PagingListAsyncHandleSync<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public PagingResult<VM> QueryPaging<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            return PagingListAsyncHandleSync<M, VM>(UiMethodEnum.QueryPagingAsync, false, null);
        }
        public PagingResult<T> QueryPaging<T>(Expression<Func<M, T>> columnMapFunc)
        {
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return PagingListAsyncHandleSync(UiMethodEnum.QueryPagingAsync, single, columnMapFunc.Compile());
        }
    }

    internal sealed class QueryPagingXImpl
        : Impler
        , IQueryPagingXAsync, IQueryPagingX
    {
        internal QueryPagingXImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            return await PagingListAsyncHandle<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
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
            return await PagingListAsyncHandle<T>(UiMethodEnum.QueryPagingAsync, single);
        }

        public PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            return PagingListAsyncHandleSync<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
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
            return PagingListAsyncHandleSync<T>(UiMethodEnum.QueryPagingAsync, single);
        }
    }

    internal sealed class PagingListXOImpl
        : Impler
        , IQueryPagingXOAsync, IQueryPagingXO
    {
        internal PagingListXOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingResult<M>> QueryPagingAsync<M>()
            where M : class
        {
            SelectMHandle<M>();
            return await PagingListAsyncHandle<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle<T>(UiMethodEnum.QueryPagingAsync, single);
        }

        public PagingResult<M> QueryPaging<M>()
            where M : class
        {
            SelectMHandle<M>();
            return PagingListAsyncHandleSync<M>(UiMethodEnum.QueryPagingAsync, false);
        }
        public PagingResult<T> QueryPaging<T>(Expression<Func<T>> columnMapFunc)
        {
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return PagingListAsyncHandleSync<T>(UiMethodEnum.QueryPagingAsync, single);
        }
    }

    internal sealed class QueryPagingSQLImpl
        : Impler
        , IQueryPagingSQLAsync, IQueryPagingSQL
    {
        public QueryPagingSQLImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<T>> QueryPagingAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryPagingAsync;
            return await DC.DSA.ExecuteReaderPagingAsync<None, T>(typeof(T).IsSingleColumn(), null);
        }

        public PagingResult<T> QueryPaging<T>()
        {
            DC.Method = UiMethodEnum.QueryPagingAsync;
            return DC.DSS.ExecuteReaderPaging<None, T>(typeof(T).IsSingleColumn(), null);
        }
    }
}
