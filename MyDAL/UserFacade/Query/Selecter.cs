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

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Selecter<M>
        : Operator
        , IWhereQ<M>
        , ISelectListAsync<M>, ISelectList<M>
        , ISelectPagingAsync<M>, ISelectPaging<M>
        , ITopAsync<M>, ITop<M>
        , IIsExistAsync, IIsExist
        where M : class
    {
        internal Selecter(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

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

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<M>> SelectListAsync()
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<VM>> SelectListAsync<VM>()
            where VM : class
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new SelectListAsyncImpl<M>(DC).SelectListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<M> SelectList()
        {
            return new SelectListImpl<M>(DC).SelectList();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<VM> SelectList<VM>()
            where VM : class
        {
            return new SelectListImpl<M>(DC).SelectList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
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
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync()
        {
            return await new IsExistAsyncImpl<M>(DC).IsExistAsync();
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistImpl<M>(DC).IsExist();
        }


    }
}
