using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace MyDAL.Core.Bases
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Operator
    {

        internal Operator(Context dc)
        {
            DC = dc;
        }

        private bool CheckWhereVal(object val, Type valType)
        {
            if (val == null)
            {
                return false;
            }
            if (valType.IsEnum && "0".Equals(val.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (valType == typeof(DateTime) && Convert.ToDateTime("0001-01-01 00:00:00.000000").Equals(val))
            {
                return false;
            }
            //|| (valType.IsList() && (val as dynamic).Count <= 0)
            if (valType.IsList() && (val as IList).Count <= 0)
            {
                return false;
            }
            //|| (valType.IsArray && (val as dynamic).Length <= 0))
            if (valType.IsArray && (val as IList).Count <= 0)
            {
                return false;
            }

            return true;
        }

        private List<(string key, string param, (object val, string valStr) val, Type valType, string colType, CompareEnum compare)> GetSetKPV<M>(object objx)
        {
            var list = new List<DicDynamic>();
            var dic = default(IDictionary<string, object>);

            //
            var mProps = typeof(M).GetProperties();
            if (objx is ExpandoObject)
            {
                dic = objx as IDictionary<string, object>;
                foreach (var mp in mProps)
                {
                    foreach (var sp in dic.Keys)
                    {
                        if (mp.Name.Equals(sp, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new DicDynamic
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
                var oProps = objx.GetType().GetProperties();
                foreach (var mp in mProps)
                {
                    foreach (var sp in oProps)
                    {
                        if (mp.Name.Equals(sp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new DicDynamic
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
            var result = new List<(string key, string param, (object val, string valStr) val, Type valType, string colType, CompareEnum compare)>();
            var columns = DC.XC.GetColumnInfos(DC.XC.GetModelKey(typeof(M).FullName));
            foreach (var prop in list)
            {
                var val = default((object val, string valStr));
                var valType = default(Type);
                var columnType = columns.First(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase)).DataType;
                if (objx is ExpandoObject)
                {
                    var obj = dic[prop.MField];
                    valType = obj.GetType();
                    val = DC.VH.ExpandoObjectValue(obj);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop.MField);
                    valType = mp.PropertyType;
                    val = DC.VH.PropertyValue(mp, objx);
                    result.Add((prop.MField, prop.VmField, val, valType, columnType, prop.Compare));
                }
            }
            return result;
        }
        private List<(ColumnParam cp, string param, (object val, string valStr) val, string colType, CompareEnum compare)> GetWhereKPV(PagingQueryOption objx, Type mType)
        {
            var result = new List<(ColumnParam cp, string param, (object val, string valStr) val, string colType, CompareEnum compare)>();
            var list = new List<DicDynamic>();

            //
            var mProps = mType.GetProperties();
            var oType = objx.GetType();
            var oProps = oType.GetProperties(XConfig.ClassSelfMember);
            foreach (var mp in mProps)
            {
                foreach (var sp in oProps)
                {
                    var spAttr = DC.AH.GetAttribute<XQueryAttribute>(oType, sp) as XQueryAttribute;
                    var spName = string.Empty;
                    var compare = CompareEnum.Equal;
                    if (spAttr != null
                        && !string.IsNullOrWhiteSpace(spAttr.Name))
                    {
                        spName = spAttr.Name;
                        compare = spAttr.Compare;
                    }
                    else
                    {
                        spName = sp.Name;
                    }

                    if (mp.Name.Equals(spName, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(new DicDynamic
                        {
                            MField = mp.Name,
                            VmField = sp.Name,
                            Compare = compare
                        });
                    }
                }
            }

            //
            var columns = DC.XC.GetColumnInfos(DC.XC.GetModelKey(mType.FullName));
            foreach (var prop in list)
            {
                var val = default((object val, string valStr));
                var valType = default(Type);
                var columnType = string.Empty;
                var ci = columns.FirstOrDefault(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase));
                if (ci != null)
                {
                    columnType = ci.DataType;
                }
                var mp = objx.GetType().GetProperty(prop.VmField);
                valType = mp.PropertyType;
                val = DC.VH.PropertyValue(mp, objx);
                if (!CheckWhereVal(val.val, valType))
                {
                    continue;
                }
                if (valType.IsList()
                    || valType.IsArray)
                {
                    var ox = DC.VH.InValue(valType, val.val);
                    valType = ox.valType;
                    val = (ox.val, string.Empty);
                }
                result.Add((new ColumnParam
                {
                    Prop = prop.MField,
                    Key = prop.MField,
                    ValType = valType,
                    ClassFullName = mType.FullName
                }, prop.VmField, val, columnType, prop.Compare));
            }
            return result;
        }

        /****************************************************************************************************************************************/

        internal Context DC { get; set; }

        /****************************************************************************************************************************************/

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> propertyFunc, F modVal, OptionEnum option)
            where M : class
        {
            var keyDic = DC.EH.FuncMFExpression(propertyFunc);
            var key = keyDic.ColumnOne;
            var val = default((object val, string valStr));
            if (modVal == null)
            {
                val = (null, string.Empty);
            }
            else
            {
                val = DC.VH.ExpandoObjectValue(modVal);
            }
            DC.Option = option;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.SetDic(typeof(M).FullName, key, key, val, typeof(F)));
        }

        internal void SetDynamicHandle<M>(object mSet)
        {
            var tuples = GetSetKPV<M>(mSet);
            var fullName = typeof(M).FullName;
            foreach (var tp in tuples)
            {
                DC.Option = OptionEnum.Set;
                DC.Compare = CompareEnum.None;
                DC.DPH.AddParameter(DC.DPH.SetDic(fullName, tp.key, tp.param, tp.val, tp.valType));
            }
        }

        internal void WhereJoinHandle(Operator op, Expression<Func<bool>> func)
        {
            var dic = op.DC.EH.FuncBoolExpression(func);
            op.DC.DPH.AddParameter(dic);
        }

        internal void WhereHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.Where;
            var field = DC.EH.FuncMBoolExpression(func);
            field.ClassFullName = typeof(M).FullName;
            DC.DPH.AddParameter(field);
        }

        internal void WhereDynamicHandle<M>(PagingQueryOption mWhere)
        {
            var mType = typeof(M);
            var tuples = GetWhereKPV(mWhere, mType);
            var count = 0;
            foreach (var tp in tuples)
            {
                count++;
                if (count == 1)
                {
                    DC.Action = ActionEnum.Where;
                }
                else
                {
                    DC.Action = ActionEnum.And;
                }

                //
                if (tp.compare == CompareEnum.Like)
                {
                    DC.Option = OptionEnum.Like;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.LikeDic(tp.cp, tp.val));
                }
                else if (tp.compare == CompareEnum.In)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.In;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.InDic(tp.cp, tp.val));
                }
                else if (tp.compare == CompareEnum.NotIn)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.NotIn;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.NotInDic(tp.cp, tp.val));
                }
                else
                {
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = tp.compare;
                    DC.DPH.AddParameter(DC.DPH.CompareDic(tp.cp, tp.val));
                }
            }
        }

        internal void AndHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.And;
            var field = DC.EH.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        internal void OrHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.Or;
            var field = DC.EH.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        internal void OrderByMF<M, F>(Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy)
            where M : class
        {
            var keyDic = DC.EH.FuncMFExpression(propertyFunc);
            switch (orderBy)
            {
                case OrderByEnum.Asc:
                    DC.Option = OptionEnum.Asc;
                    break;
                case OrderByEnum.Desc:
                    DC.Option = OptionEnum.Desc;
                    break;
            }

            DC.DPH.AddParameter(DC.DPH.OrderbyDic(keyDic.ClassFullName, keyDic.ColumnOne, keyDic.TableAliasOne));
        }

        internal void OrderByF<F>(Expression<Func<F>> func, OrderByEnum orderBy)
        {
            var keyDic = DC.EH.FuncTExpression(func);
            switch (orderBy)
            {
                case OrderByEnum.Asc:
                    DC.Option = OptionEnum.Asc;
                    break;
                case OrderByEnum.Desc:
                    DC.Option = OptionEnum.Desc;
                    break;
            }

            DC.DPH.AddParameter(DC.DPH.OrderbyDic(keyDic.ClassFullName, keyDic.ColumnOne, keyDic.TableAliasOne));
        }

        internal void DistinctHandle()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.ColumnOther;
            DC.Compare = CompareEnum.Distinct;
            DC.Func = FuncEnum.None;
            DC.DPH.AddParameter(DC.DPH.DistinctDic());
        }

        /****************************************************************************************************************************************/

    }
}
