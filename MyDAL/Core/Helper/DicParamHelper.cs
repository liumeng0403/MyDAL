using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Models.ExpPara;
using MyDAL.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyDAL.Core.表达式能力;

namespace MyDAL.Core.Helper
{
    /// <summary>
    /// sql 语句 and 参数 处理 对象
    /// </summary>
    internal class DicParamHelper
    {
        // 后续废掉
        private static DicParam SetDicBase(Context dc)
        {
            return new DicParam
            {
                //
                ID = 0,
                IsDbSet = false,
                Crud = dc.Crud,
                Action = dc.Action,
                Option = dc.Option,
                Compare = dc.Compare,
                Func = dc.Func,

                //
                GroupAction = ActionEnum.None,
                GroupRef = null
            };
        }
        private static DicParam InHelper(DicParam ui, object csVal, string csValStr)
        {
            var cp = new DicParam
            {
                //
                ID = ui.ID,
                IsDbSet = false,
                Crud = ui.Crud,
                Action = ui.Action,
                Option = ui.Option,
                Compare = ui.Compare,
                Func = ui.Func,

                //
                TbMType = ui.TbMType,
                TbAlias = ui.TbAlias,
                TbCol = ui.TbCol,
                TbColAlias = ui.TbColAlias,
                TableTwo = ui.TableTwo,
                TableAliasTwo = ui.TableAliasTwo,
                ColumnTwo = ui.ColumnTwo,
                Param = ui.Param,
                ParamRaw = ui.ParamRaw,
                CsValue = ui.CsValue,
                CsValueStr = ui.CsValueStr,
                CsType = ui.CsType,
                GroupRef = ui.GroupRef
            };

            //
            cp.CsValue = csVal;
            cp.CsValueStr = csValStr;
            cp.Compare = CompareXEnum.InHelper;

            //
            return cp;
        }

        private void InNotIn(DicParam dic)
        {
            var vals = dic.CsValue.ToString().Split(',').Select(it => it);
            foreach (var val in vals)
            {
                var dicx = InHelper(dic, val, dic.CsValueStr);
                Unique(dicx);
                dic.InItems.Add(dicx);
            }
        }
        private void Unique(DicParam dic)
        {
            //
            dic.ID = DC.DicID;
            DC.DicID++;
            if (dic.ParamRaw.IsNotBlank())
            {
                dic.Param = $"{dic.ParamRaw}_{dic.ID}";
            }

            //
            if (dic.Group != null)
            {
                foreach (var ui in dic.Group)
                {
                    if (DC.IsInParameter(ui))
                    {
                        ui.InItems = new List<DicParam>();
                    }
                    Unique(ui);
                    ui.GroupRef = dic;
                }
            }
            else if (dic.Inserts != null)
            {
                foreach (var ui in dic.Inserts)
                {
                    Unique(ui);
                }
            }
            else if (dic.Columns != null)
            {
                foreach (var ui in dic.Columns)
                {
                    Unique(ui);
                }
            }
            else if (DC.IsInParameter(dic))
            {
                InNotIn(dic);
            }
        }
        private void DbParam(DicParam ui, DicParam refDb)
        {
            if (ui.IsDbSet)
            {
                return;
            }
            ui.IsDbSet = true;

            //
            if (ui.Crud == CrudEnum.SQL)
            {
                DC.PH.GetParamInfo(ui, ui.CsType);
                return;
            }
            if (ui.CsType != null)
            {
                if (DC.IsInParameter(ui))
                {
                    ui.ParamInfo = DC.PH.GetDefault(ui.Param, ui.CsValue, DbType.String);
                }
                else
                {
                    DC.PH.GetParamInfo(ui, ui.CsType);
                }
            }
            if (ui.Group != null)
            {
                foreach (var item in ui.Group)
                {
                    DbParam(item, ui);
                }
                return;
            }
            ui.GroupRef = refDb;
            if (ui.InItems != null)
            {
                foreach (var u in ui.InItems)
                {
                    DbParam(u, refDb);
                }
            }
            if (ui.Inserts != null)
            {
                foreach (var u in ui.Inserts)
                {
                    DbParam(u, refDb);
                }
            }
            if (ui.Columns != null)
            {
                foreach (var u in ui.Columns)
                {
                    DbParam(u, refDb);
                }
            }
        }
        internal List<DicParam> FlatDics(List<DicParam> dics)
        {
            var ds = new List<DicParam>();

            //
            foreach (var d in dics)
            {
                if (DC.IsParameter(d.Action))
                {
                    if (d.Group != null)
                    {
                        ds.AddRange(FlatDics(d.Group));
                    }
                    else if (d.InItems != null)
                    {
                        ds.AddRange(FlatDics(d.InItems));
                    }
                    else if (d.Inserts != null)
                    {
                        ds.AddRange(FlatDics(d.Inserts));
                    }
                    else
                    {
                        ds.Add(d);
                    }
                }
            }

            //
            return ds;
        }
        
        // 后续废掉 , 由 属性 得到 列
        private string GetCol(Type mType, string prop)
        {
            //
            if (mType == null)
            {
                return prop;
            }

            //
            var tb = DC.XC.GetTableModel(mType);

            //
            var pc = tb?.PCA?.FirstOrDefault(it => prop.Equals(it.PropName, StringComparison.OrdinalIgnoreCase));
            if (pc != null)
            {
                return pc.ColName;
            }
            else
            {
                return prop;
            }
        }

        /*******************************************************************************************************/

        internal Context DC { get; set; }
        internal DicParamHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

        internal void ResetParameter()
        {
            DC.AlreadyOutput = false;
            DC.IsSetParam = false;
            DC.DicID = 1;
            DC.Parameters.Clear();
        }
        internal void SetParameter()
        {
            if (DC.Parameters != null)
            {
                foreach (var ui in DC.Parameters)
                {
                    DbParam(ui, null);
                }
            }
        }
        internal void AddParameter(DicParam dic)
        {
            if (dic.Crud == CrudEnum.SQL)
            {
                DC.Parameters.Add(dic);
            }
            else
            {
                if (DC.IsInParameter(dic))
                {
                    dic.InItems = new List<DicParam>();
                }
                Unique(dic);
                DC.Parameters.Add(dic);

                //
                DC.Compare = CompareXEnum.None;
                DC.Func = FuncEnum.None;
            }
        }
        internal DbParamInfo GetParameters(List<DicParam> list)
        {
            //
            var paras = new DbParamInfo();
            foreach (var db in list)
            {
                if (DC.IsParameter(db.Action))
                {
                    if (db.Group != null)
                    {
                        paras.Add(GetParameters(db.Group));
                    }
                    else if (db.Inserts != null)
                    {
                        paras.Add(GetParameters(db.Inserts));
                    }
                    else if (DC.IsInParameter(db))
                    {
                        paras.Add(GetParameters(db.InItems));
                    }
                    else
                    {
                        paras.Add(db.ParamInfo);
                    }
                }
            }

            //
            return paras;
        }

        /*******************************************************************************************************/

        internal DicParam CharLengthDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;

            return dic;
        }

        internal DicParam TrimDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;

            return dic;
        }

        internal DicParam DateFormatDic(ColumnParam cp, ValueInfo v, string format)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbColAlias = cp.Key;
            dic.TbAlias = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Format = format;

            return dic;
        }
        internal DicParam CsToStringDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);
            dic.TbColAlias = cp.Key;
            dic.TbAlias = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Format = string.Empty;

            return dic;
        }

        internal DicParam CompareDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;

            return dic;
        }

        internal DicParam InDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;

            return dic;
        }

        internal DicParam LikeDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;

            return dic;
        }

        internal DicParam OneEqualOneDic(ValueInfo v, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.TbCol = "OneEqualOne";
            dic.Param = "OneEqualOne";
            dic.ParamRaw = "OneEqualOne";
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = valType;

            return dic;
        }

        internal DicParam IsNullDic(ColumnParam cp)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbAlias = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = null;
            dic.CsValueStr = string.Empty;
            dic.CsType = cp.ValType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam SelectMemberInitDic(ColumnParam cp, string colAlias)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbAlias = cp.Alias;
            dic.TbMProp = cp.Prop;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.TbColAlias = colAlias;

            return dic;
        }

        internal DicParam DistinctDic()
        {
            var dic = SetDicBase(DC);

            return dic;
        }

        // 后续废掉
        internal DicParam CountDic(Type mType, string key, string alias = "")
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbCol = GetCol(mType, key);  // key;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicParam OrderbyDic(Type mType, string key, string alias)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbCol = GetCol(mType, key);  // key;
            dic.TbAlias = alias;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam InsertDic(Type mType, List<DicParam> list)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.Inserts = list;

            return dic;
        }
        internal DicParam InsertHelperDic(Type mType, string key, ValueInfo v, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbCol = GetCol(mType, key); //key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = valType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam SetDic(Type mType, string key, string param, ValueInfo v, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbCol = GetCol(mType, key); // key;
            dic.Param = param;
            dic.ParamRaw = param;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = valType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam OnDic(ColumnParam cp1, ColumnParam cp2)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp1.TbMType;
            dic.TbCol = cp1.Key;
            dic.TbAlias = cp1.Alias;
            dic.ColumnTwo = cp2.Key;
            dic.TableAliasTwo = cp2.Alias;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam TableDic(Type mType, string alias)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            return dic;
        }

        internal DicParam ColumnDic(ColumnParam cp)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = cp.TbMType;
            dic.TbAlias = cp.Alias;
            dic.TbCol = GetCol(cp.TbMType, cp.Key);  // cp.Key;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.TbMProp = cp.Prop;

            return dic;
        }
        internal DicParam ColumnDic(string columnOne, string tableAliasOne, Type mType, string prop)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbMProp = prop;
            dic.TbCol = GetCol(mType, columnOne);  // columnOne;
            dic.TbAlias = tableAliasOne;

            return dic;
        }

        internal DicParam JoinColumnDic(Type mType, string key, string alias, string prop)
        {
            var dic = SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbMProp = prop;
            dic.TbCol = GetCol(mType, key);  // key;
            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam GroupDic(ActionEnum action)
        {
            var dic = SetDicBase(DC);
            dic.Group = new List<DicParam>();
            dic.GroupAction = action;
            return dic;
        }

        internal DicParam SelectColumnDic(List<DicParam> cols)
        {
            var dic = SetDicBase(DC);
            dic.Columns = cols;
            return dic;
        }

    }
}
