using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Delete;
using Yunyong.DataExchange.UserFacade.Join;
using Yunyong.DataExchange.UserFacade.Query;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
{
    public static class AndEx
    {

        /*************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereD<M> And<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereU<M> And<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereQ<M> And<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        /// <summary>
        /// 过滤条件 https://www.cnblogs.com/Meng-NET/p/9978336.html 
        /// </summary>
        public static WhereX And(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.And;
            where.WhereJoinHandle(where, compareFunc);
            return where;
        }

    }
}
