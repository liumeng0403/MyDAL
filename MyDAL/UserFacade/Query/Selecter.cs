using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    public sealed class Selecter<M>
        : Operator, IAll<M>, IAllPagingList<M>, ITop<M>
        where M : class
    {
        internal Selecter(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <returns>返回全表数据</returns>
        public async Task<List<M>> AllAsync()
        {
            return await new AllImpl<M>(DC).AllAsync();
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <returns>返回全表数据</returns>
        public async Task<List<VM>> AllAsync<VM>()
            where VM : class
        {
            return await new AllImpl<M>(DC).AllAsync<VM>();
        }
        public async Task<List<F>> AllAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new AllImpl<M>(DC).AllAsync<F>(propertyFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<M>> PagingAllListAsync(int pageIndex, int pageSize)
        {
            return await new AllPagingListImpl<M>(DC).PagingAllListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<VM>> PagingAllListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new AllPagingListImpl<M>(DC).PagingAllListAsync<VM>(pageIndex, pageSize);
        }
        public async Task<PagingList<T>> PagingAllListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new AllPagingListImpl<M>(DC).PagingAllListAsync<T>(pageIndex, pageSize, columnMapFunc);
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
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc) 
        {
            return await new TopImpl<M>(DC).TopAsync<T>(count, columnMapFunc);
        }
    }
}
