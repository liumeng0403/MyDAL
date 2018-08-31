using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Core.Update
{
    public class Setter<M>: Operator
    {
        internal Setter(DbContext dc)
        {
            DC = dc;
        }

        public Setter<M> Set<F>(Expression<Func<M, F>> func, F newVal)
        {
            var key = DC.EH.ExpressionHandle(func);
            DC.AddConditions(new DicModel
            {
                KeyOne = key,
                Param=key,
                Value = DC.GH.GetTypeValue(newVal.GetType(), newVal),
                Option = OptionEnum.Set,
                Action = ActionEnum.Set,
                Crud= CrudTypeEnum.Update
            });
            return this;
        }
        public Setter<M> Set(object mSet)
        {
            DynamicSetHandle<M>(mSet);
            return this;
        }

        public Setter<M> Change<F>(Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            var key = DC.EH.ExpressionHandle(func);
            DC.AddConditions(new DicModel
            {
                KeyOne = key,
                Param=key,
                Value = DC.GH.GetTypeValue(modifyVal.GetType(), modifyVal),
                Option = DC.SqlProvider.GetChangeOption(change),
                Action = ActionEnum.Change,
                Crud= CrudTypeEnum.Update
            });
            return this;
        }


        public UpdateFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func,CrudTypeEnum.Update);
            return new UpdateFilter<M>(DC);
        }


    }
}
