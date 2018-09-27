using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Helper;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public class WhereX
        : Operator, IQueryFirstOrDefaultX, IQueryListX, IQueryPagingListX
    {

        internal WhereX(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 多表单条数据查询
        /// </summary>
        public async Task<M> QueryFirstOrDefaultAsync<M>()
        {
            return await new QueryFirstOrDefaultXImpl(DC).QueryFirstOrDefaultAsync<M>();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func)
        {
            return await new QueryFirstOrDefaultXImpl(DC).QueryFirstOrDefaultAsync<VM>(func);
        }

        public async Task<List<M>> QueryListAsync<M>()
        {
            SelectMHandle<M>();
            return (await SqlHelper.QueryAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }
        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync<M>(int pageIndex, int pageSize)
        {
            SelectMHandle<M>();
            var result = new PagingList<M>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            var result = new PagingList<VM>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync<M>(PagingQueryOption option)
        {
            SelectMHandle<M>();
            OrderByOptionHandle(option);
            var result = new PagingList<M>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option);
            var result = new PagingList<VM>();
            result.PageIndex = option.PageIndex;
            result.PageSize = option.PageSize;
            var paras = DC.GetParameters();
            var sql = DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryPagingListAsync, result.PageIndex, result.PageSize);
            result.TotalCount = await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras);
            result.Data = (await SqlHelper.QueryAsync<VM>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }

    }
}
