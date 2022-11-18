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
    public sealed class Selecter<M>
        : Operator
        , IWhereQ<M>
        , ISelectList<M>
        , ISelectPaging<M>
        , ITop<M>
        , IIsExist
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
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<M> SelectList()
        {
            return new SelectListImpl<M>(DC).SelectList();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会查询表中所有数据！！！", false)]
        public List<VM> SelectList<VM>()
            where VM : class
        {
            return new SelectListImpl<M>(DC).SelectList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".SelectList() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        /// 请参阅: <see langword=".IsExist() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistImpl<M>(DC).IsExist();
        }


    }
}
