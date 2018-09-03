using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Core.Join
{
    public class JoinX: Operator
    {

        internal JoinX(DbContext dc)
        {
            DC = dc;
        }

        public OnX InnerJoin<M>(out M m,string alias)
        {
            m = Activator.CreateInstance<M>();
            DC.AddConditions(new DicModel
            {
                TableOne = DC.SqlProvider.GetTableName(m),
                AliasOne = alias,
                Action = ActionEnum.InnerJoin,
                Crud= CrudTypeEnum.Join
            });
            return new OnX(DC);
        }

        public OnX LeftJoin<M>(out M m,string alias)
        {
            m = Activator.CreateInstance<M>();
            DC.AddConditions(new DicModel
            {
                TableOne = DC.SqlProvider.GetTableName(m),
                AliasOne = alias,
                Action = ActionEnum.LeftJoin,
                Crud = CrudTypeEnum.Join
            });
            return new OnX(DC);
        }

        public QueryFilter Where(Expression<Func<bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func, ActionEnum.Where);
            field.Crud = CrudTypeEnum.Join;
            DC.AddConditions(field);
            return new QueryFilter(DC);
        }

    }
}
