using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class OnX : Operator, IMethodObject
    {

        internal OnX(DbContext dc)
            : base(dc)
        { }

        public FromX On(Expression<Func<bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func, ActionEnum.On);
            field.Crud = CrudTypeEnum.Join;
            DC.AddConditions(field);
            return new FromX(DC);
        }

    }
}
