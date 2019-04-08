using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Delete;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using HPC.DAL.UserFacade.Update;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class OrEx
    {

        /****************************************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereD<M> Or<M>(this WhereD<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereU<M> Or<M>(this WhereU<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereQ<M> Or<M>(this WhereQ<M> where, Expression<Func<M, bool>> compareFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Or;
            where.OrHandle(compareFunc);
            return where;
        }

        /****************************************************************************************************************************************/

        /// <summary>
        /// 请参阅: <see langword=".Where() &amp; .And() &amp; .Or() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static WhereX Or(this WhereX where, Expression<Func<bool>> compareFunc)
        {
            where.DC.Action = ActionEnum.Or;
            where.WhereJoinHandle(where, compareFunc);
            return where;
        }

    }
}
