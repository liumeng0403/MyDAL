using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using System;
using System.Linq.Expressions;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class FromX : Operator, IMethodObject
    {

        internal FromX(DbContext dc)
            : base(dc)
        { }

        public OnX InnerJoin<M>(Expression<Func<M>> func)
        {
            var dic = DC.EH.ExpressionHandle(func)[0];
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
                ClassFullName = m.GetType().FullName,
                TableAliasOne = alias,
                Action = ActionEnum.LeftJoin,
                Crud = CrudTypeEnum.Join
            });
            return new OnX(DC);
        }

    }
}
