using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Core.Join
{
    public class FromX: Operator
    {
        internal FromX(DbContext dc)
        {
            DC = dc;
        }

        public JoinX From<M>(out M m,string alias)
        {
            m = Activator.CreateInstance<M>();
            DC.AddConditions(new DicModel
            {
                TableOne = DC.SqlProvider.GetTableName(m),
                AliasOne = alias,
                Action = ActionEnum.From,
                Crud= CrudTypeEnum.Join
            });
            return new JoinX(DC);
        }
    }
}
