using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class OnX: Operator
    {

        internal OnX(DbContext dc)
        {
            DC = dc;
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
