using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace MyDAL.Core.Bases
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public abstract class Operator
    {

        internal Operator(Context dc)
        {
            DC = dc;
        }

        private List<SetParam> GetSetKPV<M>(object objx)
        {
            var list = new List<SetDic>();
            var dic = default(IDictionary<string, object>);

            //
            var tbm = DC.XC.GetTableModel(typeof(M));
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
                                VmField = mp.Name
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
                                VmField = mp.Name
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
                        ValType = valType
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
                        ValType = valType
                    });
                }
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
            DC.DPH.AddParameter(DC.DPH.SetDic(typeof(M), key, key, val, typeof(F)));
        }
        internal void SetDynamicHandle<M>(object mSet)
        {
            var tuples = GetSetKPV<M>(mSet);
            foreach (var tp in tuples)
            {
                DC.Option = OptionEnum.Set;
                DC.Compare = CompareXEnum.None;
                DC.DPH.AddParameter(DC.DPH.SetDic(typeof(M), tp.Key, tp.Param, tp.Val, tp.ValType));
            }
        }

        internal void MT_WhereAndOr(Operator op, Expression<Func<bool>> func)
        {
            if (func == null)
            {
                throw XConfig.EC.Exception(XConfig.EC._002, $"Where/And/Or -- 过滤条件【Expression<Func<bool>> func】,不可为 null ！请使用【()=>true】表示无 where 条件查询！");
            }
            var dic = op.DC.XE.FuncBoolExpression(func);
            op.DC.DPH.AddParameter(dic);
        }
        internal void ST_WhereAndOr<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            if (func == null)
            {
                throw XConfig.EC.Exception(XConfig.EC._042, $"Where/And/Or -- 过滤条件【Expression<Func<M, bool>> func】,不可为 null ！请使用【it=>true】表示无 where 条件查询！");
            }
            var field = DC.XE.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        private void AddOrderByParam(DicParam keyDic, OrderByEnum orderBy)
        {
            switch (orderBy)
            {
                case OrderByEnum.Asc:
                    DC.Option = OptionEnum.Asc;
                    break;
                case OrderByEnum.Desc:
                    DC.Option = OptionEnum.Desc;
                    break;
            }

            DC.DPH.AddParameter(DC.DPH.OrderbyDic(keyDic.TbMType, keyDic.TbCol, keyDic.TbAlias));
        }
        internal void OrderByMF<M, F>(Expression<Func<M, F>> propertyFunc, OrderByEnum orderBy)
            where M : class
        {
            var keyDic = DC.XE.FuncMFExpression(propertyFunc);
            AddOrderByParam(keyDic, orderBy);
        }
        internal void OrderByF<F>(Expression<Func<F>> func, OrderByEnum orderBy)
        {
            var keyDic = DC.XE.FuncTExpression(func);
            AddOrderByParam(keyDic, orderBy);
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
