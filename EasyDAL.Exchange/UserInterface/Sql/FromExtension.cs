using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class FromExtension
    {

        public static FromX From<M>(this Joiner join, Expression<Func<M>> tableModelFunc)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.EH.FuncMExpression(tableModelFunc)[0];
            join.DC.AddConditions(dic);
            return new FromX(join.DC);
        }

    }
}
