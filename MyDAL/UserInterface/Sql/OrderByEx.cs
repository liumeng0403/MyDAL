using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class OrderByEx
    {

        /**************************************************************************************************************/

        public static OrderByQ<M> OrderBy<M, F>(this WhereQ<M> where, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.OrderByMF(propertyFunc, orderBy);
            return new OrderByQ<M>(where.DC);
        }

        public static ThenOrderByQ<M> ThenOrderBy<M, F>(this OrderByQ<M> orderByQ, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            orderByQ.DC.Action = ActionEnum.OrderBy;
            orderByQ.OrderByMF(propertyFunc, orderBy);
            return new ThenOrderByQ<M>(orderByQ.DC);
        }

        public static OrderByQO<M> OrderBy<M, F>(this WhereQO<M> where, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.OrderByMF(propertyFunc, orderBy);
            return new OrderByQO<M>(where.DC);
        }

        /**************************************************************************************************************/

        public static OrderByX OrderBy<F>(this OnX onX,Expression<Func<F>> propertyFunc,OrderByEnum orderBy= OrderByEnum.Desc)
        {
            onX.DC.Action = ActionEnum.OrderBy;
            onX.OrderByF(propertyFunc, orderBy);
            return new OrderByX(onX.DC);
        }

        public static OrderByX OrderBy<F>(this WhereX whereX, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            whereX.DC.Action = ActionEnum.OrderBy;
            whereX.OrderByF(propertyFunc, orderBy);
            return new OrderByX(whereX.DC);
        }

        public static ThenOrderByX ThenOrderBy<F>(this OrderByX orderByX,Expression<Func<F>> propertyFunc,OrderByEnum orderBy = OrderByEnum.Desc)
        {
            orderByX.DC.Action = ActionEnum.OrderBy;
            orderByX.OrderByF(propertyFunc, orderBy);
            return new ThenOrderByX(orderByX.DC);
        }

    }
}
