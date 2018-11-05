using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class OnExtension
    {

        public static OnX On(this JoinX join, Expression<Func<bool>> compareFunc)
        {
            join.DC.Action = ActionEnum.On;
            var field = join.DC.EH.FuncBoolExpression(compareFunc);
            join.DC.DPH.AddParameter(field);
            return new OnX(join.DC);
        }

    }
}
