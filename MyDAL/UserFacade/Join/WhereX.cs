using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Bases.Facades;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using HPC.DAL.Interfaces.IAsyncs;
using HPC.DAL.Interfaces.ISyncs;
using HPC.DAL.Interfaces.Segments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Join
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereX
        : WhereBase
        , IOrderByX
        , IQueryOneXAsync, IQueryOneX
        , IQueryListXAsync, IQueryListX
        , IQueryPagingXAsync, IQueryPagingX
        , ITopXAsync, ITopX
        , IIsExistXAsync, IIsExistX
        , ICountXAsync, ICountX
    {

        internal WhereX(Context dc)
            : base(dc)
        { }

        public OrderByX OrderBySegment
        {
            get
            {
                return new OrderByX(DC);
            }
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> QueryOneAsync<M>(IDbTransaction tran = null)
            where M : class
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync<M>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync(columnMapFunc,tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M QueryOne<M>(IDbTransaction tran = null)
            where M : class
        {
            return new QueryOneXImpl(DC).QueryOne<M>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T QueryOne<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new QueryOneXImpl(DC).QueryOne(columnMapFunc,tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync<M>(IDbTransaction tran = null)
            where M : class
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync<M>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync(columnMapFunc,tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> QueryList<M>(IDbTransaction tran = null)
            where M : class
        {
            return new QueryListXImpl(DC).QueryList<M>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new QueryListXImpl(DC).QueryList(columnMapFunc,tran);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync<M>(pageIndex, pageSize,tran);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync(pageIndex, pageSize, columnMapFunc,tran);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize, IDbTransaction tran = null)
            where M : class
        {
            return new QueryPagingXImpl(DC).QueryPaging<M>(pageIndex, pageSize,tran);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new QueryPagingXImpl(DC).QueryPaging(pageIndex, pageSize, columnMapFunc,tran);
        }

        public async Task<int> CountAsync(IDbTransaction tran = null)
        {
            return await new CountXAsyncImpl(DC).CountAsync(tran);
        }
        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc, IDbTransaction tran = null)
        {
            return await new CountXAsyncImpl(DC).CountAsync(propertyFunc, tran);
        }

        public int Count(IDbTransaction tran = null)
        {
            return new CountXImpl(DC).Count(tran);
        }
        public int Count<F>(Expression<Func<F>> propertyFunc, IDbTransaction tran = null)
        {
            return new CountXImpl(DC).Count(propertyFunc, tran);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> TopAsync<M>(int count, IDbTransaction tran = null)
            where M : class
        {
            return await new TopXAsyncImpl(DC).TopAsync<M>(count,tran);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return await new TopXAsyncImpl(DC).TopAsync(count, columnMapFunc,tran);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public List<M> Top<M>(int count, IDbTransaction tran = null)
            where M : class
        {
            return new TopXImpl(DC).Top<M>(count,tran);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new TopXImpl(DC).Top(count, columnMapFunc,tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync(IDbTransaction tran = null)
        {
            return await new IsExistXAsyncImpl(DC).IsExistAsync(tran);
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist(IDbTransaction tran = null)
        {
            return new IsExistXImpl(DC).IsExist(tran);
        }
    }
}
