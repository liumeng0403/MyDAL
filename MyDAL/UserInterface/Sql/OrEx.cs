using MyDAL.Core.Enums;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class OrEx
    {

        /****************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereD<M> Or<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereU<M> Or<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereQ<M> Or<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereX Or(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.Or;
            where.WhereJoinHandle(where, compareFunc);
            return where;
        }

    }
}
