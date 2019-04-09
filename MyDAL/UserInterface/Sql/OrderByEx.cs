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

    }
}
