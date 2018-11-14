using MyDAL.Core.Enums;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class XAnd
    {

        /*************************************************************************************************************************************/

        /// <summary>
        /// 与 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public static WhereD<M> And<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.DC.OP.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        /// <summary>
        /// 与条件
        /// </summary>
        /// <param name="func">格式: it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109")</param>
        public static WhereU<M> And<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.DC.OP.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        /// <summary>
        /// 与条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.DateTime_大于等于</param>
        public static WhereQ<M> And<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.And;
            where.DC.OP.AndHandle(compareFunc);
            return where;
        }

        /*************************************************************************************************************************************/

        public static WhereX And(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.And;
            where.DC.OP.WhereJoinHandle(where, compareFunc);
            return where;
        }

    }
}
