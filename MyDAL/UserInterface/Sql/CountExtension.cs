using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Query;

namespace Yunyong.DataExchange
{
    public static class CountExtension
    {


        /// <summary>
        /// select count(column)
        /// </summary>
        /// <param name="func">格式: it => it.Id</param>
        public static CountQ<M> Count<M, F>(this WhereQ<M> where, Expression<Func<M, F>> propertyFunc)
            where M : class
        {
            where.DC.Action = ActionEnum.Select;
            var keyDic = where.DC.EH.FuncMFExpression(propertyFunc)[0];
            var key = keyDic.ColumnOne;
            where.DC.Option = OptionEnum.Count;
            where.DC.Compare = CompareEnum.None;
            where.DC.AddConditions(where.DC.DH.CountDic(keyDic.ClassFullName, key));
            return new CountQ<M>(where.DC);
        }


    }
}
