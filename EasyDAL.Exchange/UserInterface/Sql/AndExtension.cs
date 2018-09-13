using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.UserFacade.Delete;
using EasyDAL.Exchange.UserFacade.Join;
using EasyDAL.Exchange.UserFacade.Query;
using EasyDAL.Exchange.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class AndExtension
    {


        /// <summary>
        /// 与 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public static DeleteFilter<M> And<M>(this DeleteFilter<M> deleteFilter, Expression<Func<M, bool>> func)
        {
            deleteFilter.DC.OP. AndHandle(func, CrudTypeEnum.Delete);
            return deleteFilter;
        }


        /// <summary>
        /// 与条件
        /// </summary>
        /// <param name="func">格式: it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109")</param>
        public static UpdateFilter<M> And<M>(this UpdateFilter<M> updateFilter, Expression<Func<M, bool>> func)
        {
            updateFilter.DC.OP. AndHandle(func, CrudTypeEnum.Update);
            return updateFilter;
        }


        /// <summary>
        /// 与条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn >= WhereTest.DateTime_大于等于</param>
        public static  QueryFilter<M> And<M>(this QueryFilter<M> queryFilter,  Expression<Func<M, bool>> func)
        {
            queryFilter.DC.OP. AndHandle(func, CrudTypeEnum.Query);
            return queryFilter;
        }

        public static QueryFilterX And(this QueryFilterX queryFilterX, Expression<Func<bool>> func)
        {
            var field = queryFilterX. DC.EH.ExpressionHandle(func, ActionEnum.And);
            field.Crud = CrudTypeEnum.Join;
            queryFilterX. DC.AddConditions(field);
            return queryFilterX;
        }

    }
}
