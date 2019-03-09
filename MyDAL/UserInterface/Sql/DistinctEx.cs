﻿using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class DistinctEx
    {
        public static DistinctQ<M> Distinct<M>(this Queryer<M> selecter)
            where M : class
        {
            selecter.DistinctHandle();
            return new DistinctQ<M>(selecter.DC);
        }

        public static DistinctQ<M> Distinct<M>(this WhereQ<M> where)
            where M : class
        {
            where.DistinctHandle();
            return new DistinctQ<M>(where.DC);
        }

        public static DistinctQO<M> Distinct<M>(this WhereQO<M> where)
            where M : class
        {
            where.DistinctHandle();
            return new DistinctQO<M>(where.DC);
        }

        public static DistinctQ<M> Distinct<M>(this OrderByQ<M> orderBy)
            where M : class
        {
            orderBy.DistinctHandle();
            return new DistinctQ<M>(orderBy.DC);
        }

        public static DistinctQO<M> Distinct<M>(this OrderByQO<M> order)
            where M : class
        {
            order.DistinctHandle();
            return new DistinctQO<M>(order.DC);
        }

        public static DistinctQ<M> Distinct<M>(this ThenOrderByQ<M> orderBy)
            where M : class
        {
            orderBy.DistinctHandle();
            return new DistinctQ<M>(orderBy.DC);
        }

        public static DistinctQO<M> Distinct<M>(this ThenOrderByQO<M> order)
            where M:class
        {
            order.DistinctHandle();
            return new DistinctQO<M>(order.DC);
        }

        public static DistinctX Distinct(this WhereX where)
        {
            where.DistinctHandle();
            return new DistinctX(where.DC);
        }

        public static DistinctXO Distinct(this WhereXO where)
        {
            where.DistinctHandle();
            return new DistinctXO(where.DC);
        }

    }
}
