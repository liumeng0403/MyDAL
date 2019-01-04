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

        private List<(string key, string param, ValueInfo val, Type valType, string colType, CompareEnum compare)> GetSetKPV<M>(object objx)
        {
            var list = new List<SetDic>();
            var dic = default(IDictionary<string, object>);

            //
            var tbm = DC.XC.GetTableModel(DC.XC.GetModelKey(typeof(M).FullName));
            if (objx is ExpandoObject)
            {
                dic = objx as IDictionary<string, object>;
                foreach (var mp in tbm.TbMProps)
                {
                    foreach (var sp in dic.Keys)
                    {
                        if (mp.Name.Equals(sp, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new SetDic
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
                foreach (var mp in tbm.TbMProps)
                {
                    foreach (var sp in oProps)
                    {
                        if (mp.Name.Equals(sp.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            list.Add(new SetDic
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
            var result = new List<(string key, string param, ValueInfo val, Type valType, string colType, CompareEnum compare)>();
            foreach (var prop in list)
            {
                var val = default(ValueInfo);
                var valType = default(Type);
                var columnType = tbm.TbCols.First(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase)).DataType;
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
        private List<XQueryParam> GetWhereKPV(PagingQueryOption query, string tFullName)
        {
            var result = new List<XQueryParam>();
            var list = new List<XQueryDic>();

            //
            var tbm = DC.XC.GetTableModel(DC.XC.GetModelKey(tFullName));
            var qType = query.GetType();
            var queryPs = qType.GetProperties(XConfig.ClassSelfMember);
            foreach (var tp in tbm.TbMProps)
            {
                foreach (var qp in queryPs)
                {
                    var cName = string.Empty;
                    var compare = CompareEnum.Equal;
                    if (DC.AH.GetAttribute<XQueryAttribute>(qType, qp) is XQueryAttribute xqA
                        && !string.IsNullOrWhiteSpace(xqA.Column))
                    {
                        cName = xqA.Column;
                        compare = xqA.Compare;
                    }
                    else
                    {
                        cName = qp.Name;
                    }

                    if (tp.Name.Equals(cName, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(new XQueryDic
                        {
                            MField = tp.Name,
                            VmField = qp.Name,
                            Compare = compare
                        });
                    }
                }
            }

            //
            foreach (var prop in list)
            {
                var val = default(ValueInfo);
                var valType = default(Type);
                var columnType = string.Empty;
                var ci = tbm.TbCols.FirstOrDefault(it => it.ColumnName.Equals(prop.MField, StringComparison.OrdinalIgnoreCase));
                if (ci != null)
                {
                    columnType = ci.DataType;
                }
                var mp = query.GetType().GetProperty(prop.VmField);
                valType = mp.PropertyType;
                val = DC.VH.PropertyValue(mp, query);
                if (!CheckWhereVal(val.Val, valType))
                {
                    continue;
                }
                if (valType.IsList()
                    || valType.IsArray)
                {
                    var ox = DC.VH.InValue(valType, val.Val);
                    valType = ox.valType;
                    val = new ValueInfo
                    {
                        Val = ox.val,
                        ValStr = string.Empty
                    };
                }
                result.Add(new XQueryParam
                {
                    Cp = new ColumnParam
                    {
                        Prop = prop.MField,
                        Key = prop.MField,
                        ValType = valType,
                        ClassFullName = tFullName
                    },
                    Param = prop.VmField,
                    Val = val,
                    ColType = columnType,
                    Compare = prop.Compare
                });
            }
            return result;
        }

        /****************************************************************************************************************************************/

        internal Context DC { get; set; }

        /****************************************************************************************************************************************/

        internal void SetChangeHandle<M, F>(Expression<Func<M, F>> propertyFunc, F modVal, OptionEnum option)
            where M : class
        {
            var keyDic = DC.XE.FuncMFExpression(propertyFunc);
            var key = keyDic.ColumnOne;
            var val = default(ValueInfo);
            if (modVal == null)
            {
                val = null;
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
            var dic = op.DC.XE.FuncBoolExpression(func);
            op.DC.DPH.AddParameter(dic);
        }

        internal void WhereHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.Where;
            var field = DC.XE.FuncMBoolExpression(func);
            field.ClassFullName = typeof(M).FullName;
            DC.DPH.AddParameter(field);
        }

        internal void WhereDynamicHandle<M>(PagingQueryOption mWhere)
        {
            var mType = typeof(M);
            var tuples = GetWhereKPV(mWhere, mType.FullName);
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
                if (tp.Compare == CompareEnum.Like)
                {
                    DC.Option = OptionEnum.Like;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.LikeDic(tp.Cp, tp.Val));
                }
                else if (tp.Compare == CompareEnum.In)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.In;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.InDic(tp.Cp, tp.Val));
                }
                else if (tp.Compare == CompareEnum.NotIn)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.NotIn;
                    DC.Compare = CompareEnum.None;
                    DC.DPH.AddParameter(DC.DPH.NotInDic(tp.Cp, tp.Val));
                }
                else
                {
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = tp.Compare;
                    DC.DPH.AddParameter(DC.DPH.CompareDic(tp.Cp, tp.Val));
                }
            }
        }

        internal void AndHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.And;
            var field = DC.XE.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        internal void OrHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.Or;
            var field = DC.XE.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        internal void OrderByMF<M, F>(Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy)
            where M : class
        {
            var keyDic = DC.XE.FuncMFExpression(propertyFunc);
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
            var keyDic = DC.XE.FuncTExpression(func);
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
