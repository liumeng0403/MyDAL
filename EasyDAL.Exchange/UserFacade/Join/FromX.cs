using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class FromX : Operator, IMethodObject
    {
        internal FromX(DbContext dc)
            : base(dc)
        { }

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
