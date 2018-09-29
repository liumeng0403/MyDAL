using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class QueryPagingListImpl<M>
        : Impler, IQueryPagingList<M>
    {
        internal QueryPagingListImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, M>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }
    }

    internal class QueryPagingListOImpl<M>
        : Impler, IQueryPagingListO<M>
    {
        internal QueryPagingListOImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync(PagingQueryOption option)
        {
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, M>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
        {
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }
    }

    internal class QueryPagingListXImpl
        : Impler, IQueryPagingListX
    {
        internal QueryPagingListXImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync<M>(int pageIndex, int pageSize)
        {
            SelectMHandle<M>();
            var result = new PagingList<M>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            var result = new PagingList<VM>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }
    }

    internal class QueryPagingListXOImpl
        : Impler, IQueryPagingListXO
    {
        internal QueryPagingListXOImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync<M>(PagingQueryOption option)
        {
            SelectMHandle<M>();
            OrderByOptionHandle(option);
            var result = new PagingList<M>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option);
            var result = new PagingList<VM>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }
    }
}
