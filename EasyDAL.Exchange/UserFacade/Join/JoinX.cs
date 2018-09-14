using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class JoinX : Operator, IMethodObject
    {

        internal JoinX(DbContext dc)
            : base(dc)
        { }

        public OnX InnerJoin<M>(Expression<Func<M>> func)
        {
            var dic = DC.EH.ExpressionHandle(func);
            dic.Action = ActionEnum.InnerJoin;
            dic.Crud = CrudTypeEnum.Join;
            DC.AddConditions(dic);
            return new OnX(DC);
        }

        public OnX LeftJoin<M>(out M m, string alias)
        {
            m = Activator.CreateInstance<M>();
            DC.AddConditions(new DicModel
            {
                TableOne = DC.SqlProvider.GetTableName(m),
                TableClass=m.GetType().FullName,
                AliasOne = alias,
                Action = ActionEnum.LeftJoin,
                Crud = CrudTypeEnum.Join
            });
            return new OnX(DC);
        }

    }
}
