using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Constraints.Segments;
using MyDAL.Impls.Implers;

namespace MyDAL.UserFacade.Join
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereX
        : Operator
        , IOrderByX
        , ISelectOneX
        , ISelectListX
        , ISelectPagingX
        , ITopX
        , IIsExistX
        , ICountX
        , ISumX
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
        /// 请参阅: <see langword=".SelectOne() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M SelectOne<M>()
            where M : class
        {
            return new SelectOneXImpl(DC).SelectOne<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectOne() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T SelectOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new SelectOneXImpl(DC).SelectOne(columnMapFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> SelectList<M>()
            where M : class
        {
            return new SelectListXImpl(DC).SelectList<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        /// 请参阅: <see langword=".IsExist() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistXImpl(DC).IsExist();
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        public int Count()
        {
            return new CountXImpl(DC).Count();
        }
        public int Count<F>(Expression<Func<F>> propertyFunc)
        {
            return new CountXImpl(DC).Count(propertyFunc);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
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
