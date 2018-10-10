using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.ExpressionX;
using Yunyong.DataExchange.UserFacade.Query;

namespace Yunyong.DataExchange
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
            where.DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,key));
            return new CountQ<M>(where.DC);
        }


    }
}
