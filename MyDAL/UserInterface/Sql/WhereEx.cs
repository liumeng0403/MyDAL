using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Delete;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using HPC.DAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class WhereEx
    {

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
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
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Where<M>(this Queryer<M> selecter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WhereHandle(compareFunc);
            return new WhereQ<M>(selecter.DC);
        }

        public static WhereQO<M> Where<M>(this Queryer<M> selecter, PagingOption option)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WherePagingHandle(option);

            selecter.DC.PageIndex = option.PageIndex;
            selecter.DC.PageSize = option.PageSize;

            return new WhereQO<M>(selecter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX Where(this OnX on, Expression<Func<bool>> compareFunc)
        {
            on.DC.Action = ActionEnum.Where;
            on.WhereJoinHandle(on, compareFunc);
            return new WhereX(on.DC);
        }

        public static WhereXO Where(this OnX on, PagingOption option)
        {
            on.DC.Action = ActionEnum.Where;
            on.WherePagingHandle(option);

            on.DC.PageIndex = option.PageIndex;
            on.DC.PageSize = option.PageSize;

            return new WhereXO(on.DC);

        }

    }
}
