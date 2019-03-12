using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryPagingImpl<M>
        : Impler, IQueryPaging<M>
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
    }

    internal class QueryPagingOImpl<M>
        : Impler, IQueryPagingO<M>
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
    }

    internal class QueryPagingXImpl
        : Impler, IQueryPagingX
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
    }

    internal class PagingListXOImpl
        : Impler, IQueryPagingXO
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
    }

    internal class QueryPagingSQLImpl
        : Impler, IQueryPagingSQL
    {
        public QueryPagingSQLImpl(Context dc)
            : base(dc)
        { }

        public async Task<PagingResult<T>> QueryPagingAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryPagingAsync;
            return await DC.DS.ExecuteReaderPagingAsync<None, T>(typeof(T).IsSingleColumn(), null);
        }
    }
}
