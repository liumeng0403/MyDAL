using HPC.DAL.Core.Bases;
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
        private static OrderByQ<M> OrderByQ<M, F>(Operator op, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy)
            where M : class
        {
            op.DC.Action = ActionEnum.OrderBy;
            op.OrderByMF(propertyFunc, orderBy);
            return new OrderByQ<M>(op.DC);
        }

        public static OrderByQ<M> OrderBy<M, F>(this Queryer<M> target, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            return OrderByQ(target, propertyFunc, orderBy);
        }
        public static OrderByQ<M> OrderBy<M, F>(this WhereQ<M> target, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            return OrderByQ(target, propertyFunc, orderBy);
        }
        public static OrderByQ<M> ThenOrderBy<M, F>(this OrderByQ<M> target, Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
            where M : class
        {
            return OrderByQ(target, propertyFunc, orderBy);
        }

        /**************************************************************************************************************/

        private static OrderByX OrderByX<F>(Operator op, Expression<Func<F>> propertyFunc, OrderByEnum orderBy)
        {
            op.DC.Action = ActionEnum.OrderBy;
            op.OrderByF(propertyFunc, orderBy);
            return new OrderByX(op.DC);
        }

        public static OrderByX OrderBy<F>(this OnX target, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            return OrderByX(target, propertyFunc, orderBy);
        }
        public static OrderByX OrderBy<F>(this WhereX target, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            return OrderByX(target, propertyFunc, orderBy);
        }
        public static OrderByX ThenOrderBy<F>(this OrderByX target, Expression<Func<F>> propertyFunc, OrderByEnum orderBy = OrderByEnum.Desc)
        {
            return OrderByX(target, propertyFunc, orderBy);
        }

    }
}
