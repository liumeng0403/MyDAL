using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        protected void DynamicSetHandle<M>(object mSet)
        {
            var list = new List<string>();
            var dic = default(IDictionary<string, object>);

            if (mSet is ExpandoObject)
            {
                dic = mSet as IDictionary<string,object>;
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in dic.Keys)
                    {
                        if (mp.Name.Equals(sp, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(mp.Name);
                        }
                    }
                }
            }
            else
            {
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in mSet.GetType().GetProperties())
                    {
                        if (mp.Name.Equals(sp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(mp.Name);
                        }
                    }
                }
            }

            //
            foreach (var prop in list)
            {
                var val = string.Empty;
                if (mSet is ExpandoObject)
                {
                    var obj = dic[prop];
                    var mt = obj.GetType();
                    val = DC.GH.GetTypeValue(mt, obj);
                }
                else
                {
                    var mp = mSet.GetType().GetProperty(prop);
                    var mt = mp.GetType();
                    val = DC.GH.GetTypeValue(mt, mp, mSet);
                }
                DC.AddConditions(new DicModel
                {
                    KeyOne = prop,
                    Param = prop,
                    Value = val,
                    Action = ActionEnum.Set,
                    Option = OptionEnum.Set,
                    Crud = CrudTypeEnum.Update
                });
            }
        }
        
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
