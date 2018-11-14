using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class XOn
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
