using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class OnExtension
    {

        public static OnX On(this JoinX join, Expression<Func<bool>> func)
        {
            var field = join.DC.EH.ExpressionHandle(func, ActionEnum.On);
            field.Crud = CrudTypeEnum.Join;
            join.DC.AddConditions(field);
            return new OnX(join.DC);
        }

    }
}
