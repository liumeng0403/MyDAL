using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    public sealed class ThenOrderByQ<M>
        : Operator, Interfaces.IList<M>, IPagingList<M>, IPagingListO<M>,ITop<M>
            where M : class
    {

        internal ThenOrderByQ(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> ListAsync()
        {
            return await new ListImpl<M>(DC).ListAsync();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<List<VM>> ListAsync<VM>()
            where VM:class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> ListAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM:class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>(columnMapFunc);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> ListAsync(int topCount)
        {
            return await new ListImpl<M>(DC).ListAsync(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> ListAsync<VM>(int topCount)
            where VM : class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> ListAsync<VM>(int topCount, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>(topCount, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize)
        {
            return await new PagingListImpl<M>(DC).PagingListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM:class
        {
            return await new PagingListImpl<M>(DC).PagingListAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> columnMapFunc)
            where VM:class
        {
            return await new PagingListImpl<M>(DC).PagingListAsync<VM>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync(PagingQueryOption option)
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM:class
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<VM>(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> columnMapFunc)
            where VM:class
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<VM>(option, columnMapFunc);
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
    }
}
