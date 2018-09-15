using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.UserFacade.Delete;
using Yunyong.DataExchange.UserFacade.Join;
using Yunyong.DataExchange.UserFacade.Query;
using Yunyong.DataExchange.UserFacade.Update;

namespace Yunyong.DataExchange
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

        public static WhereX Where(this OnX on, Expression<Func<bool>> func)
        {
            var field = on.DC.EH.ExpressionHandle(func, ActionEnum.Where);
            field.Crud = CrudTypeEnum.Join;
            on.DC.AddConditions(field);
            return new WhereX(on.DC);
        }

    }
}
