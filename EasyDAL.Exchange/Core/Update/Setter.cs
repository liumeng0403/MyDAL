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
            SetChangeHandle<M, F>(func, newVal, ActionEnum.Set, OptionEnum.Set);
            return this;
        }
        public Setter<M> Set(object mSet)
        {
            DynamicSetHandle<M>(mSet);
            return this;
        }

        public Setter<M> Change<F>(Expression<Func<M, F>> func, F modifyVal, ChangeEnum change)
        {
            SetChangeHandle<M, F>(func, modifyVal, ActionEnum.Change, DC.SqlProvider.GetChangeOption(change));
            return this;
        }


        public UpdateFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func,CrudTypeEnum.Update);
            return new UpdateFilter<M>(DC);
        }


    }
}
