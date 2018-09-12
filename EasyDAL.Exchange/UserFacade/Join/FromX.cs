using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class FromX: Operator,IMethodObject
    {
        internal FromX(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
        }

        public JoinX From<M>(Expression<Func<M>> func)
        {
            var dic = DC.EH.ExpressionHandle(func);
            dic.Action = ActionEnum.From;
            dic.Crud = CrudTypeEnum.Join;
            DC.AddConditions(dic);
            return new JoinX(DC);
        }
    }
}
