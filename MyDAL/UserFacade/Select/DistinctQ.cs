using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class DistinctQ<M>
        : Operator
        , ISelectOne<M>
        , ISelectListAsync<M>, ISelectList<M>
        , ISelectPagingAsync<M>, ISelectPaging<M>
        , ITopAsync<M>, ITop<M>
        ,ICountAsync<M>,ICount<M>
        where M : class
    {
        internal DistinctQ(Context dc)
            : base(dc)
        { }

        /*--------------------------------------------------------------------------------------------------------SelectOne-----------*/
        
        /// <summary>
        /// 请参阅: <see langword=".SelectOne() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M SelectOne()
        {
            return new SelectOneImpl<M>(DC).SelectOne();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOne() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public VM SelectOne<VM>()
            where VM : class
        {
            return new SelectOneImpl<M>(DC).SelectOne<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOne() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T SelectOne<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new SelectOneImpl<M>(DC).SelectOne<T>(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> SelectListAsync()
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<VM>> SelectListAsync<VM>()
            where VM : class
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> SelectList()
        {
            return new SelectListImpl<M>(DC).SelectList();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<VM> SelectList<VM>()
            where VM : class
        {
            return new SelectListImpl<M>(DC).SelectList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> SelectList<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new SelectListImpl<M>(DC).SelectList(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> SelectPagingAsync(int pageIndex, int pageSize)
        {
            return await new SelectPagingAsyncImpl<M>(DC).SelectPagingAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<VM>> SelectPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new SelectPagingAsyncImpl<M>(DC).SelectPagingAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> SelectPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new SelectPagingAsyncImpl<M>(DC).SelectPagingAsync<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> SelectPaging(int pageIndex, int pageSize)
        {
            return new SelectPagingImpl<M>(DC).SelectPaging(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<VM> SelectPaging<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return new SelectPagingImpl<M>(DC).SelectPaging<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return new SelectPagingImpl<M>(DC).SelectPaging<T>(pageIndex, pageSize, columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

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

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await new CountAsyncImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new CountAsyncImpl<M>(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public int Count()
        {
            return new CountImpl<M>(DC).Count();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public int Count<F>(Expression<Func<M, F>> propertyFunc)
        {
            return new CountImpl<M>(DC).Count(propertyFunc);
        }

    }
}
