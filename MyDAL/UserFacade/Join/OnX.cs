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
    public sealed class OnX
        : Operator
        , IWhereX
        , ISelectOneX
        , ISelectListX
        , ISelectPagingX
        , ITopX
        , IIsExistX
    {

        internal OnX(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".Where() 之 .WhereSegment 根据条件 动态设置 Select查询条件 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public WhereX WhereSegment
        {
            get
            {
                return new WhereX(DC);
            }
        }

        /*--------------------------------------------------------------------------------------------------------SelectOne---------*/
        
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
            return new SelectOneXImpl(DC).SelectOne<T>(columnMapFunc);
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
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistXImpl(DC).IsExist();
        }
    }
}
