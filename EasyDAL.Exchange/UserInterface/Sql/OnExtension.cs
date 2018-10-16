using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class OnExtension
    {

        public static OnX On(this JoinX join, Expression<Func<bool>> func)
        {
            join.DC.Action = ActionEnum.On;
            var field = join.DC.EH.ExpressionHandle(func);
            join.DC.AddConditions(field);
            return new OnX(join.DC);
        }

    }
}
