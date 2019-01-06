using MyDAL.Core.Enums;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 
    /// </summary>
    public static class WhereEx
    {

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereD<M> Where<M>(this Deleter<M> deleter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            deleter.DC.Action = ActionEnum.Where;
            deleter.WhereHandle(compareFunc);
            return new WhereD<M>(deleter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> Where<M>(this SetU<M> set, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            set.DC.Action = ActionEnum.Where;
            set.WhereHandle(compareFunc);
            return new WhereU<M>(set.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Where<M>(this Queryer<M> selecter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WhereHandle(compareFunc);
            return new WhereQ<M>(selecter.DC);
        }

        public static WhereQO<M> Where<M>(this Queryer<M> selecter, PagingOption pagingQuery)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WherePagingHandle(pagingQuery);

            selecter.DC.PageIndex = pagingQuery.PageIndex;
            selecter.DC.PageSize = pagingQuery.PageSize;

            return new WhereQO<M>(selecter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX Where(this OnX on, Expression<Func<bool>> compareFunc)
        {
            on.DC.Action = ActionEnum.Where;
            on.WhereJoinHandle(on, compareFunc);
            return new WhereX(on.DC);
        }

        public static WhereX Where(this OnX on, PagingOption pagingQuery)
        {
            on.DC.Action = ActionEnum.Where;
            on.WherePagingHandle(pagingQuery);

            on.DC.PageIndex = pagingQuery.PageIndex;
            on.DC.PageSize = pagingQuery.PageSize;

            return null;

        }

    }
}
