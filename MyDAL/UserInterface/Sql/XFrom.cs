using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class XFrom
    {

        public static FromX From<M>(this Joiner join, Expression<Func<M>> tableModelFunc)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.EH.FuncTExpression(tableModelFunc)[0];
            join.DC.DPH.AddParameter(dic);
            return new FromX(join.DC);
        }

    }
}
