using MyDAL.Core.Enums;
using MyDAL.Core.ExpressionX;
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
            var keyDic = where.DC.EH.ExpressionHandle(ActionEnum.Select,func)[0];
            var key = keyDic.ColumnOne;
            where.DC.Option = OptionEnum.Count;
            where.DC.Compare = CompareEnum.None;
            where.DC.AddConditions(where.DC.DH.CountDic(keyDic.ClassFullName,key));
            return new CountQ<M>(where.DC);
        }


    }
}
