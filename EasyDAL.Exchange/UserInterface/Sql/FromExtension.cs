using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class FromExtension
    {

        public static FromX From<M>(this Joiner join, Expression<Func<M>> func)
        {
            var dic = join.DC.EH.ExpressionHandle( ActionEnum.From, func)[0];
            //dic.Action = ActionEnum.From;
            //dic.Crud = CrudTypeEnum.Join;
            join.DC.AddConditions(dic);
            return new FromX(join.DC);
        }

    }
}
