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
        
        protected void WhereHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        protected void AndHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        protected void OrHandle<T>(Expression<Func<T, bool>> func,CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            field.Crud = crud;
            DC.AddConditions(field);
        }
    }
}
