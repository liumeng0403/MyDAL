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
            deleter.ST_WhereAndOr(compareFunc);
            return new WhereD<M>(deleter.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereD<M> And<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereD<M> Or<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> Where<M>(this SetU<M> set, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            set.DC.Action = ActionEnum.Where;
            set.ST_WhereAndOr(compareFunc);
            return new WhereU<M>(set.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> And<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> Or<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Where<M>(this Queryer<M> selecter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.ST_WhereAndOr(compareFunc);
            return new WhereQ<M>(selecter.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> And<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Or<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.ST_WhereAndOr(compareFunc);
            return where;
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX Where(this OnX on, Expression<Func<bool>> compareFunc)
        {
            on.DC.Action = ActionEnum.Where;
            on.MT_WhereAndOr(on, compareFunc);
            return new WhereX(on.DC);
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX And(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.And;
            where.MT_WhereAndOr(where, compareFunc);
            return where;
        }
        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX Or(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.Or;
            where.MT_WhereAndOr(where, compareFunc);
            return where;
        }

    }
}
