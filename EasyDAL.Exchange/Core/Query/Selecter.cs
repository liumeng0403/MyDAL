using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using Rainbow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Query
{
    public class Selecter<M>: Operator
    {
        internal Selecter(DbContext dc)
        {
            DC = dc;
        }

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.CreatedOn</param>
        public QueryFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func,CrudTypeEnum.Query);
            return new QueryFilter<M>(DC);
        }
        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
        /// </summary>
        public QueryFilter<M> Where(object mWhere)
        {
            DynamicWhereHandle<M>(mWhere);
            return new QueryFilter<M>(DC);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <returns>返回全表数据</returns>
        public async Task<List<M>> QueryAllAsync()
        {
            return await QueryAllAsyncHandle<M, M>();
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <returns>返回全表数据</returns>
        public async Task<List<VM>> QueryAllAsync<VM>()
        {
            return await QueryAllAsyncHandle<M, VM>();
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, M>(pageIndex, pageSize, SqlTypeEnum.QueryAllPagingListAsync);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, SqlTypeEnum.QueryAllPagingListAsync);
        }

    }
}
