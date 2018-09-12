using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EasyDAL.Exchange.Common;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class OnX: Operator,IMethodObject
    {

        internal OnX(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
        }

        public JoinX On(Expression<Func<bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func, ActionEnum.On);
            field.Crud = CrudTypeEnum.Join;
            DC.AddConditions(field);
            return new JoinX(DC);
        }

    }
}
