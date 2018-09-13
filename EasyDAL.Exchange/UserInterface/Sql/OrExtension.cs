using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.UserFacade.Delete;
using EasyDAL.Exchange.UserFacade.Query;
using EasyDAL.Exchange.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class OrExtension
    {


        /// <summary>
        /// 或 条件
        /// </summary>
        /// <param name="func">格式: it => it.Id == m.Id</param>
        public static DeleteFilter<M> Or<M>(this DeleteFilter<M> deleteFilter, Expression<Func<M, bool>> func)
        {
            deleteFilter.DC.OP. OrHandle(func, CrudTypeEnum.Delete);
            return deleteFilter;
        }


        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074")</param>
        public static UpdateFilter<M> Or<M>(this UpdateFilter<M> updateFilter, Expression<Func<M, bool>> func)
        {
            updateFilter.DC.OP. OrHandle(func, CrudTypeEnum.Update);
            return updateFilter;
        }


        /// <summary>
        /// 或条件
        /// </summary>
        /// <param name="func">格式: it => it.AgentLevel == testQ.AgentLevelXX</param>
        public static QueryFilter<M> Or<M>(this QueryFilter<M> queryFilter, Expression<Func<M, bool>> func)
        {
            queryFilter.DC.OP. OrHandle(func, CrudTypeEnum.Query);
            return queryFilter;
        }

    }
}
