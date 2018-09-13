using EasyDAL.Exchange.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class OrderByExtension
    {

        /**************************************************************************************************************/

        public static OrderBy<M> OrderBy<M, F>(this QueryFilter<M> queryFilter, Expression<Func<M,F>> func,OrderByEnum orderBy= OrderByEnum.Desc)
        {
            queryFilter.DC.OP.OrderByHandle(func, orderBy);
            return new OrderBy<M>(queryFilter.DC);
        }

        public static ThenOrderBy<M> ThenOrderBy<M, F>(this OrderBy<M> orderByer, Expression<Func<M, F>> func, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            orderByer.DC.OP.OrderByHandle(func, orderBy);
            return new ThenOrderBy<M>(orderByer.DC);
        }

        /**************************************************************************************************************/

    }
}
