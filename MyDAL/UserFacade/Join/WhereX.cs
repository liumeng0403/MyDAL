using MyDAL.Core.Bases;
using MyDAL.Impls.ImplAsyncs;
using MyDAL.Impls.ImplSyncs;
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
        , ISelectOneXAsync, ISelectOneX
        , ISelectListXAsync, ISelectListX
        , ISelectPagingXAsync, ISelectPagingX
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
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> SelectOneAsync<M>()
            where M : class
        {
            return await new SelectOneXAsyncImpl(DC).SelectOneAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> SelectOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new SelectOneXAsyncImpl(DC).SelectOneAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M SelectOne<M>()
            where M : class
        {
            return new SelectOneXImpl(DC).SelectOne<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T SelectOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new SelectOneXImpl(DC).SelectOne(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> SelectListAsync<M>()
            where M : class
        {
            return await new SelectListXAsyncImpl(DC).SelectListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new SelectListXAsyncImpl(DC).SelectListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> SelectList<M>()
            where M : class
        {
            return new SelectListXImpl(DC).SelectList<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> SelectList<T>(Expression<Func<T>> columnMapFunc)
        {
            return new SelectListXImpl(DC).SelectList(columnMapFunc);
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
            return await new SelectPagingXAsyncImpl(DC).SelectPagingAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<T>> SelectPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new SelectPagingXAsyncImpl(DC).SelectPagingAsync(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> SelectPaging<M>(int pageIndex, int pageSize)
            where M : class
        {
            return new SelectPagingXImpl(DC).SelectPaging<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<T> SelectPaging<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return new SelectPagingXImpl(DC).SelectPaging(pageIndex, pageSize, columnMapFunc);
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
