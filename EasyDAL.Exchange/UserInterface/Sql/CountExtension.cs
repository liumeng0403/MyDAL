using MyDAL.ExpressionX;
using MyDAL.UserFacade.Query;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class CountExtension
    {


        /// <summary>
        /// select count(column)
        /// </summary>
        /// <param name="func">格式: it => it.Id</param>
        public static CountQ<M> Count<M,F>(this WhereQ<M> where, Expression<Func<M, F>> func)
        {
            var keyDic = where.DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            where.DC.AddConditions(DicHandle.ConditionCountHandle(key));
            return new CountQ<M>(where.DC);
        }


    }
}
