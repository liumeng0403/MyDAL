﻿using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    public sealed class OrderByQ<M>
        : Operator, IQueryList<M>, IPagingList<M>,ITop<M>
            where M : class
    {
        internal OrderByQ(Context dc)
            : base(dc)
        { }


        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync()
        {
            return await new QueryListImpl<M>(DC).QueryListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM:class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryListImpl<M>(DC).QueryListAsync(columnMapFunc);
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
        public async Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new PagingListImpl<M>(DC).PagingListAsync<T>(pageIndex, pageSize, columnMapFunc);
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
