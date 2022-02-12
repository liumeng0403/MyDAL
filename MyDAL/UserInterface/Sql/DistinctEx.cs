﻿using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class DistinctEx
    {
        public static DistinctQ<M> Distinct<M>(this Selecter<M> selecter)
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

        /******************************************************************************************************************/

        public static DistinctX Distinct(this WhereX where)
        {
            where.DistinctHandle();
            return new DistinctX(where.DC);
        }

    }
}
