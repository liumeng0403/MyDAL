using HPC.DAL.Core.Bases;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using HPC.DAL.Interfaces.Segments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Queryer<M>
        : Operator
        , IWhereQ<M>
        , IQueryListAsync<M>, IQueryList<M>
        , IQueryPagingAsync<M>, IQueryPaging<M>
        , ITopAsync<M>, ITop<M>
        , IIsExistAsync, IIsExist
        where M : class
    {
        internal Queryer(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 请参阅: <see langword=".Where() 之 .WhereSegment 根据条件 动态设置 Select查询条件 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public WhereQ<M> WhereSegment
        {
            get
            {
                return new WhereQ<M>(DC);
            }
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<M>> QueryListAsync(IDbTransaction tran = null)
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<VM>> QueryListAsync<VM>(IDbTransaction tran = null)
            where VM : class
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync(columnMapFunc,tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<M> QueryList(IDbTransaction tran = null)
        {
            return new QueryListImpl<M>(DC).QueryList(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<VM> QueryList<VM>(IDbTransaction tran = null)
            where VM : class
        {
            return new QueryListImpl<M>(DC).QueryList<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new QueryListImpl<M>(DC).QueryList(columnMapFunc,tran);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize)
        {
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync(pageIndex, pageSize);
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
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> QueryPaging(int pageIndex, int pageSize)
        {
            return new QueryPagingImpl<M>(DC).QueryPaging(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return new QueryPagingImpl<M>(DC).QueryPaging<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryPagingImpl<M>(DC).QueryPaging<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<M>> TopAsync(int count)
        {
            return await new TopAsyncImpl<M>(DC).TopAsync(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            return await new TopAsyncImpl<M>(DC).TopAsync<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            return await new TopAsyncImpl<M>(DC).TopAsync<T>(count, columnMapFunc);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<M> Top(int count)
        {
            return new TopImpl<M>(DC).Top(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<VM> Top<VM>(int count)
            where VM : class
        {
            return new TopImpl<M>(DC).Top<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            return new TopImpl<M>(DC).Top<T>(count, columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync(IDbTransaction tran = null)
        {
            return await new IsExistAsyncImpl<M>(DC).IsExistAsync(tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist(IDbTransaction tran = null)
        {
            return new IsExistImpl<M>(DC).IsExist(tran);
        }


    }
}
