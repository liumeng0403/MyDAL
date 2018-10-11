using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.ExpressionX;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Core
{
    public abstract class Operator
        : IObjectMethod
    {

        internal Operator(Context dc)
        {
            DC = dc;
            DC.OP = this;
        }

        private bool CheckQueryVal()
        {
            return false;
        }

        private List<(string key, string param, object val, Type valType, string colType, CompareEnum compare)> GetSetKPV<M>(object objx)
        {
            var list = new List<DicQueryModel>();
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
                            list.Add(new DicQueryModel
                            {
                                MField = mp.Name,
                                VmField = mp.Name,
                                Compare = CompareEnum.Equal
                            });
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
                            list.Add(new DicQueryModel
                            {
                                MField = mp.Name,
                                VmField = mp.Name,
                                Compare = CompareEnum.Equal
                            });
                        }
                    }
                }
            }

            //
            var result = new List<(string key, string param, object val, Type valType, string colType, CompareEnum compare)>();
            var columns = DC.SC.GetColumnInfos(DC.SC.GetKey(typeof(M).FullName, DC.Conn.Database));
            foreach (var prop in list)
            {
                var val = default(object);
                var valType = default(Type);
                var columnType = columns.First(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase)).DataType;
                if (objx is ExpandoObject)
                {
                    var obj = dic[prop.MField];
                    valType = obj.GetType();
                    val = DC.GH.GetTypeValue(obj);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop.MField);
                    valType = mp.PropertyType;
                    val = DC.GH.GetTypeValue(mp, objx);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
            }
            return result;
        }
        private List<(string key, string param, object val, Type valType, string colType, CompareEnum compare)> GetWhereKPV<M>(object objx)
        {
            var list = new List<DicQueryModel>();
            var dic = default(IDictionary<string, object>);

            if (objx is PagingQueryOption)
            {
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in objx.GetType().GetProperties(Configs.ClassSelfMember))
                    {
                        var spAttr = DC.AH.GetAttribute<QueryColumnAttribute>(objx.GetType(), sp) as QueryColumnAttribute;
                        var spName = string.Empty;
                        var compare = CompareEnum.Equal;
                        if (spAttr != null
                            && !string.IsNullOrWhiteSpace(spAttr.ColumnName))
                        {
                            spName = spAttr.ColumnName;
                            compare = spAttr.CompareCondition;
                        }
                        else
                        {
                            spName = sp.Name;
                        }

                        if (mp.Name.Equals(spName, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new DicQueryModel
                            {
                                MField = mp.Name,
                                VmField = sp.Name,
                                Compare = compare
                            });
                        }
                    }
                }
            }
            else if (objx is ExpandoObject)
            {
                dic = objx as IDictionary<string, object>;
                foreach (var mp in typeof(M).GetProperties())
                {
                    foreach (var sp in dic.Keys)
                    {
                        if (mp.Name.Equals(sp, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new DicQueryModel
                            {
                                MField = mp.Name,
                                VmField = mp.Name,
                                Compare = CompareEnum.Equal
                            });
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
                            list.Add(new DicQueryModel
                            {
                                MField = mp.Name,
                                VmField = mp.Name,
                                Compare = CompareEnum.Equal
                            });
                        }
                    }
                }
            }

            //
            var result = new List<(string key, string param, object val, Type valType, string colType, CompareEnum compare)>();
            var columns = DC.SC.GetColumnInfos(DC.SC.GetKey(typeof(M).FullName, DC.Conn.Database));
            foreach (var prop in list)
            {
                var val = default(object);
                var valType = default(Type);
                var columnType = columns.First(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase)).DataType;
                if (objx is PagingQueryOption)
                {
                    var mp = objx.GetType().GetProperty(prop.VmField);
                    valType = mp.PropertyType;
                    val = DC.GH.GetTypeValue(mp, objx);
                    if (val == null
                        || (valType.IsEnum && "0".Equals(val.ToString(), StringComparison.OrdinalIgnoreCase))
                        || (valType == typeof(DateTime) && Convert.ToDateTime("0001-01-01 00:00:00.000000").Equals(val)))
                    {
                        continue;
                    }
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
                else if (objx is ExpandoObject)
                {
                    var obj = dic[prop.MField];
                    valType = obj.GetType();
                    val = DC.GH.GetTypeValue(obj);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop.MField);
                    valType = mp.PropertyType;
                    val = DC.GH.GetTypeValue(mp, objx);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
            }
            return result;
        }

        /****************************************************************************************************************************************/

        internal Context DC { get; set; }

        /****************************************************************************************************************************************/

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> func, F modVal, ActionEnum action, OptionEnum option)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            var val = default(object);
            if (modVal == null)
            {
                val = null;
            }
            else
            {
                val = DC.GH.GetTypeValue(modVal);
            }
            DC.AddConditions(new DicModelUI
            {
                ClassFullName = typeof(M).FullName,
                ColumnOne = key,
                Param = key,
                ParamRaw = key,
                CsValue = val,
                CsType = typeof(F),
                Option = option,
                Action = action,
                Crud = CrudTypeEnum.Update
            });
        }

        internal void SetDynamicHandle<M>(object mSet)
        {
            var tuples = GetSetKPV<M>(mSet);
            var fullName = typeof(M).FullName;
            foreach (var tp in tuples)
            {
                DC.AddConditions(new DicModelUI
                {
                    ClassFullName = fullName,
                    ColumnOne = tp.key,
                    Param = tp.param,
                    ParamRaw = tp.param,
                    CsValue = tp.val,
                    CsType = tp.valType,
                    Action = ActionEnum.Update,
                    Option = OptionEnum.Set,
                    Crud = CrudTypeEnum.Update
                });
            }
        }

        internal void WhereJoinHandle(Operator op, Expression<Func<bool>> func, ActionEnum action)
        {
            var dic = op.DC.EH.ExpressionHandle(func, action, CrudTypeEnum.Join);
            dic.Crud = CrudTypeEnum.Join;
            op.DC.AddConditions(dic);
        }

        internal void WhereHandle<T>(Expression<Func<T, bool>> func, CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(crud, ActionEnum.Where, func);
            field.ClassFullName = typeof(T).FullName;
            field.Action = ActionEnum.Where;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void WhereDynamicHandle<M>(object mWhere)
        {
            var tuples = GetWhereKPV<M>(mWhere);
            var count = 0;
            var action = ActionEnum.None;
            var fullName = typeof(M).FullName;
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
                var crud = CrudTypeEnum.Query;

                //
                if (tp.compare == CompareEnum.Like)
                {
                    DC.AddConditions(DicHandle.CallLikeHandle(crud, action, fullName, tp.key, string.Empty, tp.val, tp.valType));
                }
                else if (tp.compare == CompareEnum.In)
                {
                    DC.AddConditions(DicHandle.CallInHandle(crud, action, fullName, tp.key, string.Empty, tp.val, tp.valType));
                }
                else
                {
                    DC.AddConditions(DicHandle.BinaryCompareHandle(crud, action, fullName, tp.key, string.Empty, tp.val, tp.valType, tp.compare));
                }
            }
        }

        internal void AndHandle<T>(Expression<Func<T, bool>> func, CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(crud, ActionEnum.And, func);
            field.Action = ActionEnum.And;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void OrHandle<T>(Expression<Func<T, bool>> func, CrudTypeEnum crud)
        {
            var field = DC.EH.ExpressionHandle(crud, ActionEnum.Or, func);
            field.Action = ActionEnum.Or;
            field.Crud = crud;
            DC.AddConditions(field);
        }

        internal void OrderByHandle<M, F>(Expression<Func<M, F>> func, OrderByEnum orderBy)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            var option = OptionEnum.None;
            switch (orderBy)
            {
                case OrderByEnum.Asc:
                    option = OptionEnum.Asc;
                    break;
                case OrderByEnum.Desc:
                    option = OptionEnum.Desc;
                    break;
            }

            DC.AddConditions(new DicModelUI
            {
                ColumnOne = key,
                Option = option,
                Action = ActionEnum.OrderBy,
                Crud = CrudTypeEnum.Query
            });
        }

        protected void OrderByOptionHandle(PagingQueryOption option)
        {
            if (option.OrderBys != null
              && option.OrderBys.Any())
            {
                foreach (var item in option.OrderBys)
                {
                    if (!string.IsNullOrWhiteSpace(item.Field))
                    {
                        var op = OptionEnum.None;
                        if (item.Desc)
                        {
                            op = OptionEnum.Desc;
                        }
                        else
                        {
                            op = OptionEnum.Asc;
                        }
                        DC.AddConditions(new DicModelUI
                        {
                            ColumnOne = item.Field,
                            Action = ActionEnum.OrderBy,
                            Option = op
                        });
                    }
                }
            }
        }

        /****************************************************************************************************************************************/

    }
}
