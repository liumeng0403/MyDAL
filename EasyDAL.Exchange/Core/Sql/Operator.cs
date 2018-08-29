using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Core.Sql
{
    public abstract class Operator
    {
        
        internal Operator()
        {
        }

        internal DbContext DC { get; set; }
        
        protected void WhereHandle<T>(Expression<Func<T, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            DC.AddConditions(field);
        }

        protected void AndHandle<T>(Expression<Func<T, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            DC.AddConditions(field);
        }

        protected void OrHandle<T>(Expression<Func<T, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            DC.AddConditions(field);
        }
    }
}
