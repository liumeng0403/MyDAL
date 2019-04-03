﻿using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereQ<M>
        : Operator
        , IQueryOne<M>, IQueryList<M>, IQueryPaging<M>
        , IIsExist, ICount<M>
        , ITop<M>
        , ISum<M>
        where M : class
    {
        internal WhereQ(Context dc)
            : base(dc) { }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync()
        {
            return await new IsExistImpl<M>(DC).IsExistAsync();
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await new CountImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new CountImpl<M>(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return await new SumImpl<M>(DC).SumAsync(propertyFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> QueryOneAsync()
        {
            return await new QueryOneImpl<M>(DC).QueryOneAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<VM> QueryOneAsync<VM>()
            where VM : class
        {
            return await new QueryOneImpl<M>(DC).QueryOneAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryOneImpl<M>(DC).QueryOneAsync<T>(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync()
        {
            return await new QueryListImpl<M>(DC).QueryListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM : class
        {
            return await new QueryListImpl<M>(DC).QueryListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        public async Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize)
        {
            return await new QueryPagingImpl<M>(DC).QueryPagingAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new QueryPagingImpl<M>(DC).QueryPagingAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryPagingImpl<M>(DC).QueryPagingAsync<T>(pageIndex, pageSize, columnMapFunc);
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
