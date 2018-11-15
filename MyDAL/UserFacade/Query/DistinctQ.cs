using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Query
{
    public sealed class DistinctQ<M>
        : Operator, IQueryAll<M>, IQueryAllPagingList<M>, ITop<M>, IQueryFirstOrDefault<M>, IQueryList<M>, IQueryPagingList<M>, IQueryPagingListO<M>, ICount<M>
        where M : class
    {
        internal DistinctQ(Context dc) 
            : base(dc)
        {
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <returns>返回全表数据</returns>
        public async Task<List<M>> QueryAllAsync()
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync();
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <returns>返回全表数据</returns>
        public async Task<List<VM>> QueryAllAsync<VM>()
            where VM : class
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync<VM>();
        }
        public async Task<List<F>> QueryAllAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync<F>(propertyFunc);
        }
        public async Task<List<string>> QueryAllAsync(Expression<Func<M, string>> propertyFunc)
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync(propertyFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize)
        {
            return await new QueryAllPagingListImpl<M>(DC).QueryAllPagingListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new QueryAllPagingListImpl<M>(DC).QueryAllPagingListAsync<VM>(pageIndex, pageSize);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<M>> TopAsync(int count)
        {
            return await new TopImpl<M>(DC).TopAsync(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<VM>> TopAsync<VM>(int count, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(count, columnMapFunc);
        }

        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        public async Task<M> QueryFirstOrDefaultAsync()
        {
            return await new QueryFirstOrDefaultImpl<M>(DC).QueryFirstOrDefaultAsync();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
            where VM : class
        {
            return await new QueryFirstOrDefaultImpl<M>(DC).QueryFirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new QueryFirstOrDefaultImpl<M>(DC).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> QueryListAsync()
        {
            return await new QueryListImpl<M>(DC).QueryListAsync();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM : class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>(columnMapFunc);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> QueryListAsync(int topCount)
        {
            return await new QueryListImpl<M>(DC).QueryListAsync(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>(int topCount)
            where VM : class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>(int topCount, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>(topCount, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            return await new QueryPagingListImpl<M>(DC).QueryPagingListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new QueryPagingListImpl<M>(DC).QueryPagingListAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new QueryPagingListImpl<M>(DC).QueryPagingListAsync<VM>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync(PagingQueryOption option)
        {
            return await new QueryPagingListOImpl<M>(DC).QueryPagingListAsync(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
            where VM : class
        {
            return await new QueryPagingListOImpl<M>(DC).QueryPagingListAsync<VM>(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new QueryPagingListOImpl<M>(DC).QueryPagingListAsync<VM>(option, columnMapFunc);
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<long> CountAsync()
        {
            return await new CountImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<long> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new CountImpl<M>(DC).CountAsync(propertyFunc);
        }

    }
}
