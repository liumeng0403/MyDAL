using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
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

        public static ThenOrderByQO<M> ThenOrderBy<M, F>(this OrderByQO<M> order, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByMF(propertyFunc, orderBy);
            return new ThenOrderByQO<M>(order.DC);
        }

        public static ThenOrderByQO<M> ThenOrderBy<M, F>(this ThenOrderByQO<M> order, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByMF(propertyFunc, orderBy);
            return new ThenOrderByQO<M>(order.DC);
        }

        /**************************************************************************************************************/

        public static OrderByX OrderBy<F>(this OnX onX, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
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

        public static OrderByXO OrderBy<F>(this WhereXO whereXO, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            whereXO.DC.Action = ActionEnum.OrderBy;
            whereXO.OrderByF(propertyFunc, orderBy);
            return new OrderByXO(whereXO.DC);
        }

        public static ThenOrderByX ThenOrderBy<F>(this OrderByX orderByX, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            orderByX.DC.Action = ActionEnum.OrderBy;
            orderByX.OrderByF(propertyFunc, orderBy);
            return new ThenOrderByX(orderByX.DC);
        }

        public static ThenOrderByXO ThenOrderBy<F>(this OrderByXO orderByXO, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            orderByXO.DC.Action = ActionEnum.OrderBy;
            orderByXO.OrderByF(propertyFunc, orderBy);
            return new ThenOrderByXO(orderByXO.DC);
        }

    }
}
