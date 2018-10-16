using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class FromExtension
    {

        public static FromX From<M>(this Joiner join, Expression<Func<M>> func)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.EH.FuncMExpression( func)[0];
            join.DC.AddConditions(dic);
            return new FromX(join.DC);
        }

    }
}
