using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Constraints.Segments;
using MyDAL.Impls.Implers;

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereQ<M>
        : Operator
        , IOrderByQ<M>
        , ISelectOne<M>
        , ISelectList<M>
        , ISelectPaging<M>
        , ITop<M>
        , IIsExist
        , ICount<M>
        , ISum<M>
        where M : class
    {
        internal WhereQ(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public OrderByQ<M> OrderBySegment
        {
            get
            {
                return new OrderByQ<M>(DC);
            }
        }

        /*--------------------------------------------------------------------------------------------------------SelectOne--------*/
        
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
            return new SelectOneImpl<M>(DC).SelectOne(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> SelectList()
        {
            return new SelectListImpl<M>(DC).SelectList();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<VM> SelectList<VM>()
            where VM : class
        {
            return new SelectListImpl<M>(DC).SelectList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        public bool IsExist()
        {
            return new IsExistImpl<M>(DC).IsExist();
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
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

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return new SumImpl<M>(DC).Sum(propertyFunc);
        }
        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public F? Sum<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct
        {
            return new SumImpl<M>(DC).Sum(propertyFunc);
        }

    }
}
