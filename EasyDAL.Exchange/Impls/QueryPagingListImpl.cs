using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
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
            DC.IP.ConvertDic();
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
            OrderByOptionHandle(option,typeof(M).FullName);
            DC.IP.ConvertDic();
            return await QueryPagingListAsyncHandle<M, M>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
        {
            OrderByOptionHandle(option,typeof(M).FullName);
            DC.IP.ConvertDic();
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option,typeof(M).FullName);
            DC.IP.ConvertDic();
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
            DC.IP.ConvertDic();
            var result = new PagingList<M>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await DC.DS.ExecuteReaderMultiRowAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            DC.IP.ConvertDic();
            var result = new PagingList<VM>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await DC.DS.ExecuteReaderMultiRowAsync<VM>(DC.Conn, sql[1], paras)).ToList();
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
            OrderByOptionHandle(option,typeof(M).FullName);
            DC.IP.ConvertDic();
            var result = new PagingList<M>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await DC.DS.ExecuteReaderMultiRowAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option,string.Empty);
            DC.IP.ConvertDic();
            var result = new PagingList<VM>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await DC.DS.ExecuteScalarAsync<int>(DC.Conn, sql[0], paras);
            result.Data = (await DC.DS.ExecuteReaderMultiRowAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }
    }
}
