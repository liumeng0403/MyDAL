using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange
{
    public static class OnExtension
    {

        public static FromX On(this JoinX join, Expression<Func<bool>> func)
        {
            var field = join.DC.EH.ExpressionHandle(func, ActionEnum.On);
            field.Crud = CrudTypeEnum.Join;
            join.DC.AddConditions(field);
            return new FromX(join.DC);
        }

    }
}
