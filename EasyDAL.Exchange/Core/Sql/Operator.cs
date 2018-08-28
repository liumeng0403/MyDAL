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
            DC.Conditions.Add(field);
        }

        protected void AndHandle<T>(Expression<Func<T, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            DC.Conditions.Add(field);
        }

        protected void OrHandle<T>(Expression<Func<T, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            DC.Conditions.Add(field);
        }
    }
}
