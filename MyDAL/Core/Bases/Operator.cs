using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Models.ExpPara;
using MyDAL.Core.Models.Page;
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
            if (valType.IsList() && (val as IList).Count <= 0)
            {
                return false;
            }
            if (valType.IsArray && (val as IList).Count <= 0)
            {
                return false;
            }

            return true;
        }
        private static CompareXEnum GetCompareX(CompareEnum compare, Context dc)
        {
            switch (compare)
            {
                case CompareEnum.Equal:
                    return CompareXEnum.Equal;
                case CompareEnum.NotEqual:
                    return CompareXEnum.NotEqual;
                case CompareEnum.LessThan:
                    return CompareXEnum.LessThan;
                case CompareEnum.LessThanOrEqual:
                    return CompareXEnum.LessThanOrEqual;
                case CompareEnum.GreaterThan:
                    return CompareXEnum.GreaterThan;
                case CompareEnum.GreaterThanOrEqual:
                    return CompareXEnum.GreaterThanOrEqual;
                default:
                    throw dc.Exception(XConfig.EC._002, compare.ToString());
            }
        }

        private List<SetParam> GetSetKPV<M>(object objx)
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
            var result = new List<SetParam>();
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
                    result.Add(new SetParam
                    {
                        Key = prop.MField,
                        Param = prop.VmField,
                        Val = val,
                        ValType = valType,
                        ColType = columnType,
                        Compare = prop.Compare
                    });
                }
                else
                {
                    var mp = objx.GetType().GetProperty(prop.MField);
                    valType = mp.PropertyType;
                    val = DC.VH.PropertyValue(mp, objx);
                    result.Add(new SetParam
                    {
                        Key = prop.MField,
                        Param = prop.VmField,
                        Val = val,
                        ValType = valType,
                        ColType = columnType,
                        Compare = prop.Compare
                    });
                }
            }
            return result;
        }
        private List<XQueryParam> GetWhereKPV(PagingOption query)
        {
            //
            var result = new List<XQueryParam>();
            var dics = new List<XQueryDic>();
            var queryT = query.GetType();
            var ops = DC.XC.GetPagingOption(queryT);
            foreach (var op in ops)
            {
                dics.Add(new XQueryDic
                {
                    MFullName = op.TbMType.FullName,
                    MProp = op.TbMProp.Name,
                    QCol = op.ColName,
                    QProp = op.PropName,
                    Compare = op.Compare
                });
            }

            //
            foreach (var dic in dics)
            {
                var val = default(ValueInfo);
                var valType = default(Type);
                var qp = queryT.GetProperty(dic.QProp);
                valType = qp.PropertyType;
                val = DC.VH.PropertyValue(qp, query);
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
                        Prop = dic.MProp,
                        Key = dic.QCol,
                        ValType = valType,
                        TbMFullName = dic.MFullName
                    },
                    Val = val,
                    Compare = dic.Compare
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
            var key = keyDic.TbCol;
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
            DC.Compare = CompareXEnum.None;
            DC.DPH.AddParameter(DC.DPH.SetDic(typeof(M).FullName, key, key, val, typeof(F)));
        }

        internal void SetDynamicHandle<M>(object mSet)
        {
            var tuples = GetSetKPV<M>(mSet);
            var fullName = typeof(M).FullName;
            foreach (var tp in tuples)
            {
                DC.Option = OptionEnum.Set;
                DC.Compare = CompareXEnum.None;
                DC.DPH.AddParameter(DC.DPH.SetDic(fullName, tp.Key, tp.Param, tp.Val, tp.ValType));
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
            field.TbMFullName = typeof(M).FullName;
            DC.DPH.AddParameter(field);
        }

        internal void WherePagingHandle(PagingOption query)
        {
            var tuples = GetWhereKPV(query);
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
                    DC.Compare = CompareXEnum.None;
                    DC.DPH.AddParameter(DC.DPH.LikeDic(tp.Cp, tp.Val));
                }
                else if (tp.Compare == CompareEnum.In)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.In;
                    DC.Compare = CompareXEnum.None;
                    DC.DPH.AddParameter(DC.DPH.InDic(tp.Cp, tp.Val));
                }
                else if (tp.Compare == CompareEnum.NotIn)
                {
                    DC.Option = OptionEnum.Function;
                    DC.Func = FuncEnum.NotIn;
                    DC.Compare = CompareXEnum.None;
                    DC.DPH.AddParameter(DC.DPH.NotInDic(tp.Cp, tp.Val));
                }
                else if (tp.Compare == CompareEnum.Equal
                            || tp.Compare == CompareEnum.NotEqual
                            || tp.Compare == CompareEnum.LessThan
                            || tp.Compare == CompareEnum.LessThanOrEqual
                            || tp.Compare == CompareEnum.GreaterThan
                            || tp.Compare == CompareEnum.GreaterThanOrEqual)
                {
                    DC.Option = OptionEnum.Compare;
                    DC.Compare = GetCompareX(tp.Compare, DC);
                    DC.DPH.AddParameter(DC.DPH.CompareDic(tp.Cp, tp.Val));
                }
                else
                {
                    throw DC.Exception(XConfig.EC._004, tp.Compare.ToString());
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

            DC.DPH.AddParameter(DC.DPH.OrderbyDic(keyDic.TbMFullName, keyDic.TbCol, keyDic.TbAlias));
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

            DC.DPH.AddParameter(DC.DPH.OrderbyDic(keyDic.TbMFullName, keyDic.TbCol, keyDic.TbAlias));
        }

        internal void DistinctHandle()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.ColumnOther;
            DC.Compare = CompareXEnum.Distinct;
            DC.Func = FuncEnum.None;
            DC.DPH.AddParameter(DC.DPH.DistinctDic());
        }

        /****************************************************************************************************************************************/

    }
}
