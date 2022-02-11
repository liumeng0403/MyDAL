using MyDAL.Core.Bases;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class DistinctX
        : Operator
        , ISelectOneXAsync, ISelectOneX
        , ISelectListXAsync, ISelectListX
        , ISelectPagingXAsync, ISelectPagingX
        , ITopXAsync, ITopX
        ,ICountXAsync,ICountX
    {
        internal DistinctX(Context dc)
            : base(dc)
        {
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> SelectOneAsync<M>()
            where M : class
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> SelectOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryOneXAsyncImpl(DC).QueryOneAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M SelectOne<M>()
            where M : class
        {
            return new QueryOneXImpl(DC).QueryOne<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T SelectOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new QueryOneXImpl(DC).QueryOne(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> SelectListAsync<M>()
            where M : class
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryListXAsyncImpl(DC).QueryListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> SelectList<M>()
            where M : class
        {
            return new QueryListXImpl(DC).QueryList<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> SelectList<T>(Expression<Func<T>> columnMapFunc)
        {
            return new QueryListXImpl(DC).QueryList(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> SelectPagingAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<T>> SelectPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new QueryPagingXAsyncImpl(DC).QueryPagingAsync(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> SelectPaging<M>(int pageIndex, int pageSize)
            where M : class
        {
            return new QueryPagingXImpl(DC).QueryPaging<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
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

    }
}
