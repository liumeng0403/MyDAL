using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class FromEx
    {

        public static FromX From<M>(this Queryer join, Expression<Func<M>> tableModelFunc)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.XE.FuncTExpression(tableModelFunc);
            join.DC.DPH.AddParameter(dic);
            return new FromX(join.DC);
        }

    }
}
