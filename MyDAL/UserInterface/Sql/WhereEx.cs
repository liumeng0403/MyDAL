using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Delete;
using Yunyong.DataExchange.UserFacade.Join;
using Yunyong.DataExchange.UserFacade.Query;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
{
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

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereD<M> Where<M>(this Deleter<M> deleter, object mWhere)
            where M:class
        {
            deleter.DC.Action = ActionEnum.Where;
            deleter.WhereDynamicHandle<M>(mWhere);
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

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> Where<M>(this SetU<M> set, object mWhere)
            where M:class
        {
            set.DC.Action = ActionEnum.Where;
            set.WhereDynamicHandle<M>(mWhere);
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

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Where<M>(this Queryer<M> selecter, object mWhere)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WhereDynamicHandle<M>(mWhere);
            return new WhereQ<M>(selecter.DC);
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

        //public static WhereX Where(this OnX on, object mWhere)
        //{
        //    on.DC.OP.WhereJoinHandle(on, func, ActionEnum.Where);
        //    return new WhereX(on.DC);
        //}

    }
}
