using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
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
