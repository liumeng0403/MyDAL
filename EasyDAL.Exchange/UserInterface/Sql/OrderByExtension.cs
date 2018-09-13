using EasyDAL.Exchange.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class OrderByExtension
    {

        /**************************************************************************************************************/

        public static QueryFilter<M> OrderBy<M, F>(this QueryFilter<M> queryFilter, Expression<Func<M,F>> func,OrderByEnum orderBy= OrderByEnum.Desc)
        {
            queryFilter.DC.OP.OrderByHandle(func, orderBy);
            return queryFilter;
        }

        public static QueryFilter<M> ThenOrderBy<M, F>(this QueryFilter<M> queryFilter, Expression<Func<M, F>> func, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            queryFilter.DC.OP.OrderByHandle(func, orderBy);
            return queryFilter;
        }

        /**************************************************************************************************************/

    }
}
