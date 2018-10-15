using MyDAL.Core.Enums;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class WhereExtension
    {

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id </param>
        public static WhereD<M> Where<M>(this Deleter<M> deleter, Expression<Func<M, bool>> func)
        {
            deleter.DC.OP.WhereHandle(func);
            return new WhereD<M>(deleter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.AgentId == id2</param>
        public static WhereU<M> Where<M>(this SetU<M> set, Expression<Func<M, bool>> func)
        {
            set.DC.OP.WhereHandle(func);
            return new WhereU<M>(set.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.CreatedOn</param>
        public static WhereQ<M> Where<M>(this Selecter<M> selecter, Expression<Func<M, bool>> func)
        {
            selecter.DC.OP.WhereHandle(func);
            return new WhereQ<M>(selecter.DC);
        }
        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
        /// </summary>
        public static WhereQ<M> Where<M>(this Selecter<M> selecter, object mWhere)
        {
            selecter.DC.OP.WhereDynamicHandle<M>(mWhere);
            return new WhereQ<M>(selecter.DC);
        }

        /**************************************************************************************************************/

        public static WhereX Where(this OnX on, Expression<Func<bool>> func)
        {
            on.DC.OP.WhereJoinHandle(on, func, ActionEnum.Where);
            return new WhereX(on.DC);
        }

        //public static WhereX Where(this OnX on, object mWhere)
        //{
        //    on.DC.OP.WhereJoinHandle(on, func, ActionEnum.Where);
        //    return new WhereX(on.DC);
        //}

    }
}
