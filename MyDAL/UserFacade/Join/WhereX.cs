using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using MyDAL.Interfaces.Segments;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereX
        : Operator
        , IOrderByX
        , IQueryOneXAsync, ISelectOneX
        , IQueryListXAsync, IQueryListX
        , IQueryPagingXAsync, IQueryPagingX
        , ITopXAsync, ITopX
        , IIsExistXAsync, IIsExistX
        , ICountXAsync, ICountX
        , ISumXAsync, ISumX
    {

        internal WhereX(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public OrderByX OrderBySegment
        {
            get
            {
                return new OrderByX(DC);
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> QueryOneAsync<M>()
            where M : class
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M QueryOne<M>()
            where M : class
        {
            return new QueryOneXImpl(DC).QueryOne<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T QueryOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new QueryOneXImpl(DC).QueryOne(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync<M>()
            where M : class
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> QueryList<M>()
            where M : class
        {
            return new QueryListXImpl(DC).QueryList<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc)
        {
            return new QueryListXImpl(DC).QueryList(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> QueryPaging<M>(int pageIndex, int pageSize)
            where M : class
        {
            return new QueryPagingXImpl(DC).QueryPaging<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return new QueryPagingXImpl(DC).QueryPaging(pageIndex, pageSize, columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> TopAsync<M>(int count)
            where M : class
        {
            return await new TopXAsyncImpl(DC).TopAsync<M>(count);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            return await new TopXAsyncImpl(DC).TopAsync(count, columnMapFunc);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public List<M> Top<M>(int count)
            where M : class
        {
            return new TopXImpl(DC).Top<M>(count);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            return new TopXImpl(DC).Top(count, columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync()
        {
            return await new IsExistXAsyncImpl(DC).IsExistAsync();
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistXImpl(DC).IsExist();
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public async Task<int> CountAsync()
        {
            return await new CountXAsyncImpl(DC).CountAsync();
        }
        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            return await new CountXAsyncImpl(DC).CountAsync(propertyFunc);
        }

        public int Count()
        {
            return new CountXImpl(DC).Count();
        }
        public int Count<F>(Expression<Func<F>> propertyFunc)
        {
            return new CountXImpl(DC).Count(propertyFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public async Task<F> SumAsync<F>(Expression<Func<F>> propertyFunc)
            where F : struct
        {
            return await new SumXAsyncImpl(DC).SumAsync(propertyFunc);
        }
        public async Task<F?> SumAsync<F>(Expression<Func<F?>> propertyFunc)
            where F : struct
        {
            return await new SumXAsyncImpl(DC).SumAsync(propertyFunc);
        }

        public F Sum<F>(Expression<Func<F>> propertyFunc)
            where F : struct
        {
            return new SumXImpl(DC).Sum(propertyFunc);
        }
        public F? Sum<F>(Expression<Func<F?>> propertyFunc)
            where F : struct
        {
            return new SumXImpl(DC).Sum(propertyFunc);
        }

    }
}
