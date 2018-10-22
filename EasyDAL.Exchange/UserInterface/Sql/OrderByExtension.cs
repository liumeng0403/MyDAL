using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class OrderByExtension
    {

        /**************************************************************************************************************/

        public static OrderByQ<M> OrderBy<M, F>(this WhereQ<M> where, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.DC.OP.OrderByHandle(propertyFunc, orderBy);
            return new OrderByQ<M>(where.DC);
        }

        public static ThenOrderByQ<M> ThenOrderBy<M, F>(this OrderByQ<M> orderByQ, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            orderByQ.DC.Action = ActionEnum.OrderBy;
            orderByQ.DC.OP.OrderByHandle(propertyFunc, orderBy);
            return new ThenOrderByQ<M>(orderByQ.DC);
        }

        /**************************************************************************************************************/

        public static OrderByX OrderBy<F>(this OnX join,Expression<Func<F>> propertyFunc,OrderByEnum orderBy= OrderByEnum.Desc)
        {
            join.DC.Action = ActionEnum.OrderBy;
            join.DC.OP.OrderByHandle(propertyFunc, orderBy);
            return new OrderByX(join.DC);
        }

        public static ThenOrderByX ThenOrderBy<F>(this OrderByX orderByX,Expression<Func<F>> propertyFunc,OrderByEnum orderBy = OrderByEnum.Desc)
        {
            orderByX.DC.Action = ActionEnum.OrderBy;
            orderByX.DC.OP.OrderByHandle(propertyFunc, orderBy);
            return new ThenOrderByX(orderByX.DC);
        }

    }
}
