using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Delete;
using Yunyong.DataExchange.UserFacade.Join;
using Yunyong.DataExchange.UserFacade.Query;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
{
    public static class OrExtension
    {

        /****************************************************************************************************************************************/

        /// <summary>
        /// 或 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public static WhereD<M> Or<M>(this WhereD<M> where, Expression<Func<M, bool>> func)
        {
            where.DC.Action = ActionEnum.Or;
            where.DC.OP. OrHandle(func);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074")</param>
        public static WhereU<M> Or<M>(this WhereU<M> where, Expression<Func<M, bool>> func)
        {
            where.DC.Action = ActionEnum.Or;
            where.DC.OP. OrHandle(func);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.AgentLevel == testQ.AgentLevelXX</param>
        public static WhereQ<M> Or<M>(this WhereQ<M> where, Expression<Func<M, bool>> func)
        {
            where.DC.Action = ActionEnum.Or;
            where.DC.OP. OrHandle(func);
            return where;
        }

        /****************************************************************************************************************************************/

        public static WhereX Or(this WhereX where, Expression<Func<bool>> func)
        {
            where.DC.Action = ActionEnum.Or;
            where.DC.OP.WhereJoinHandle(where, func);
            return where;
        }

    }
}
