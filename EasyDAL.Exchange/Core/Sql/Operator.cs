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

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> func, F modVal, ActionEnum action, OptionEnum option)
        {
            var key = DC.EH.ExpressionHandle(func);
            var val = string.Empty;
            if (modVal == null)
            {
                val = null;
            }
            else
            {
                val = DC.GH.GetTypeValue(modVal.GetType(), modVal);
            }
            DC.AddConditions(new DicModel
            {
                KeyOne = key,
                Param = key,
                Value = val,
                Option = option,
                Action = action,
                Crud = CrudTypeEnum.Update
            });
        }

        private List<(string key,string param,string val)> GetKPV<M>(object objx)
        {
            var list = new List<string>();
            var dic = default(IDictionary<string, object>);

            if (objx is ExpandoObject)
            {
                dic = objx as IDictionary<string, object>;
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
                    foreach (var sp in objx.GetType().GetProperties())
                    {
                        if (mp.Name.Equals(sp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(mp.Name);
                        }
                    }
                }
            }

            //
            var result = new List<(string key, string param, string val)>();
            foreach (var prop in list)
            {
                var val = string.Empty;
                if (objx is ExpandoObject)
                {
                    var obj = dic[prop];
                    var mt = obj.GetType();
                    val = DC.GH.GetTypeValue(mt, obj);
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop);
                    var mt = mp.GetType();
                    val = DC.GH.GetTypeValue(mt, mp, objx);
                }
                result.Add((prop, prop, val));
            }
            return result;
        }
        protected void DynamicSetHandle<M>(object mSet)
        {
            var tuples = GetKPV<M>(mSet);
            foreach (var tp in tuples)
            {             
                DC.AddConditions(new DicModel
                {
                    KeyOne = tp.key,
                    Param = tp.param,
                    Value = tp.val,
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

        protected void DynamicWhereHandle<M>(object mWhere)
        {
            var tuples = GetKPV<M>(mWhere);
            var count = 0;
            var action = ActionEnum.None;
            foreach (var tp in tuples)
            {
                count++;
                if (count == 1)
                {
                    action = ActionEnum.Where;
                }
                else
                {
                    action = ActionEnum.And;
                }
                DC.AddConditions(new DicModel
                {
                    KeyOne = tp.key,
                    Param = tp.param,
                    Value = tp.val,
                    Action = action,
                    Option = OptionEnum.Equal,
                    Crud = CrudTypeEnum.Query
                });
            }
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
