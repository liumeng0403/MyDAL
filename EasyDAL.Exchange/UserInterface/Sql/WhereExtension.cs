using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.UserFacade.Delete;
using EasyDAL.Exchange.UserFacade.Join;
using EasyDAL.Exchange.UserFacade.Query;
using EasyDAL.Exchange.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class WhereExtension
    {

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id </param>
        public static DeleteFilter<M> Where<M>(this Deleter<M> deleter, Expression<Func<M, bool>> func)
        {
            deleter.DC.OP.WhereHandle(func, CrudTypeEnum.Delete);
            return new DeleteFilter<M>(deleter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.AgentId == id2</param>
        public static UpdateFilter<M> Where<M>(this Setter<M> setter, Expression<Func<M, bool>> func)
        {
            setter.DC.OP.WhereHandle(func, CrudTypeEnum.Update);
            return new UpdateFilter<M>(setter.DC);
        }

        /**************************************************************************************************************/

        /// <summary>
        /// 过滤条件起点
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.CreatedOn</param>
        public static QueryFilter<M> Where<M>(this Selecter<M> selecter, Expression<Func<M, bool>> func)
        {
            selecter.DC.OP.WhereHandle(func, CrudTypeEnum.Query);
            return new QueryFilter<M>(selecter.DC);
        }
        /// <summary>
        /// 过滤条件起点 -- 设置多个条件
        /// </summary>
        public static QueryFilter<M> Where<M>(this Selecter<M> selecter, object mWhere)
        {
            selecter.DC.OP.WhereDynamicHandle<M>(mWhere);
            return new QueryFilter<M>(selecter.DC);
        }

        /**************************************************************************************************************/

        public static QueryFilterX Where(this FromX joinX, Expression<Func<bool>> func)
        {
            var field = joinX.DC.EH.ExpressionHandle(func, ActionEnum.Where);
            field.Crud = CrudTypeEnum.Join;
            joinX.DC.AddConditions(field);
            return new QueryFilterX(joinX.DC);
        }

    }
}
