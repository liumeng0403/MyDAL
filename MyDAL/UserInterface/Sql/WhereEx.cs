using MyDAL.Core.Enums;
using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class WhereEx
    {

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id </param>
        public static WhereD<M> Where<M>(this Deleter<M> deleter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            deleter.DC.Action = ActionEnum.Where;
            deleter.WhereHandle(compareFunc);
            return new WhereD<M>(deleter.DC);
        }

        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
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
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.AgentId == id2</param>
        public static WhereU<M> Where<M>(this SetU<M> set, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            set.DC.Action = ActionEnum.Where;
            set.WhereHandle(compareFunc);
            return new WhereU<M>(set.DC);
        }

        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
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
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.CreatedOn</param>
        public static WhereQ<M> Where<M>(this Selecter<M> selecter, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WhereHandle(compareFunc);
            return new WhereQ<M>(selecter.DC);
        }

        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
        /// </summary>
        public static WhereQ<M> Where<M>(this Selecter<M> selecter, object mWhere)
            where M : class
        {
            selecter.DC.Action = ActionEnum.Where;
            selecter.WhereDynamicHandle<M>(mWhere);
            return new WhereQ<M>(selecter.DC);
        }

        /**************************************************************************************************************/

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
