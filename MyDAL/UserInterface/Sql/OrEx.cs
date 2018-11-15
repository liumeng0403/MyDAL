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
        /// 或 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public static WhereD<M> Or<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074")</param>
        public static WhereU<M> Or<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.AgentLevel == testQ.AgentLevelXX</param>
        public static WhereQ<M> Or<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        public static WhereX Or(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.Or;
            where.WhereJoinHandle(where, compareFunc);
            return where;
        }

    }
}
