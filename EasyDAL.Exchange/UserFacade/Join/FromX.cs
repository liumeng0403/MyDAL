using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class FromX: Operator
    {
        internal FromX(DbContext dc)
        {
            DC = dc;
        }

        public JoinX From<M>(Expression<Func<M>> func)
        {
            DC.AddConditions(new DicModel
            {
                //TableOne = DC.SqlProvider.GetTableName(m),
                AliasOne ="",// alias,
                Action = ActionEnum.From,
                Crud= CrudTypeEnum.Join
            });
            return new JoinX(DC);
        }
    }
}
