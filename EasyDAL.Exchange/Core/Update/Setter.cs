using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Core.Update
{
    public class Setter<M>
    {
        private DbContext DC { get; set; }
        
        public Setter(DbContext dc)
        {
            DC = dc;
        }

        public Setter<M> Set<F>(Expression<Func<M, F>> func, F newVal)
        {
            var key = DC.EH.ExpressionHandle(func);
            //Fields.Add(key);
            DC.Conditions.Add(new DicModel<string, string>
            {
                key = key,
                Value = DC.GH.GetTypeValue(newVal.GetType(), newVal),
                Option = OptionEnum.Set,
                Action = ActionEnum.Set
            });
            return this;
        }

        public Setter<M> Change<F>(Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            var key = DC.EH.ExpressionHandle(func);
            DC.Conditions.Add(new DicModel<string, string>
            {
                key = key,
                Value = DC.GH.GetTypeValue(modifyVal.GetType(), modifyVal),
                Option = DC.OP.GetChangeOption(change),
                Action = ActionEnum.Change
            });
            return this;
        }


        public Filter<M> Where(Expression<Func<M, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            DC.Conditions.Add(field);
            return new Filter<M>(DC);
        }


    }
}
