using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
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

        public static OrderByQ<M> ThenOrderBy<M, F>(this OrderByQ<M> order, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByMF(propertyFunc, orderBy);
            return new OrderByQ<M>(order.DC);
        }

        public static OrderByQO<M> OrderBy<M, F>(this DistinctQO<M> distinct, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            distinct.DC.Action = ActionEnum.OrderBy;
            distinct.OrderByMF(propertyFunc, orderBy);
            return new OrderByQO<M>(distinct.DC);
        }

        public static OrderByQO<M> OrderBy<M, F>(this WhereQO<M> where, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.OrderByMF(propertyFunc, orderBy);
            return new OrderByQO<M>(where.DC);
        }

        public static OrderByQO<M> ThenOrderBy<M, F>(this OrderByQO<M> order, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByMF(propertyFunc, orderBy);
            return new OrderByQO<M>(order.DC);
        }

        /**************************************************************************************************************/

        public static OrderByX OrderBy<F>(this OnX on, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            on.DC.Action = ActionEnum.OrderBy;
            on.OrderByF(propertyFunc, orderBy);
            return new OrderByX(on.DC);
        }

        public static OrderByX OrderBy<F>(this WhereX where, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.OrderByF(propertyFunc, orderBy);
            return new OrderByX(where.DC);
        }

        public static OrderByX ThenOrderBy<F>(this OrderByX order, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByF(propertyFunc, orderBy);
            return new OrderByX(order.DC);
        }

        public static OrderByXO OrderBy<F>(this WhereXO where, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            where.DC.Action = ActionEnum.OrderBy;
            where.OrderByF(propertyFunc, orderBy);
            return new OrderByXO(where.DC);
        }

        public static OrderByXO ThenOrderBy<F>(this OrderByXO order, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            order.DC.Action = ActionEnum.OrderBy;
            order.OrderByF(propertyFunc, orderBy);
            return new OrderByXO(order.DC);
        }

    }
}
