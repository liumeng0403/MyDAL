using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Helper
{
    internal class DicParamHelper
    {
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
                ClassFullName = ui.ClassFullName,
                TableAliasOne = ui.TableAliasOne,
                ColumnOne = ui.ColumnOne,
                ColumnOneAlias = ui.ColumnOneAlias,
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
            cp.Func = FuncEnum.InHelper;

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
            if (!dic.ParamRaw.IsNullStr())
            {
                dic.Param = $"{dic.ParamRaw}__{dic.ID}";
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
            if (ui.ClassFullName.IsNullStr())
            {
                ui.Key = string.Empty;
            }
            else
            {
                ui.Key = DC.XC.GetModelKey(ui.ClassFullName);
                ui.TableOne = DC.XC.GetTableName(ui.Key);
            }
            if (ui.CsType != null)
            {
                if (DC.IsInParameter(ui))
                {
                    ui.ParamInfo = ParameterHelper.GetDefault(ui.Param, ui.CsValue, DbType.String);
                }
                else
                {
                    DC.PH.GetDbVal(ui, ui.CsType);
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
        private List<DicParam> FlatDics(List<DicParam> dics)
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

        /*******************************************************************************************************/

        internal Context DC { get; set; }
        internal DicParamHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

        internal void ResetParameter()
        {
            DC.Parameters = new List<DicParam>();
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
            if (DC.IsInParameter(dic))
            {
                dic.InItems = new List<DicParam>();
            }
            Unique(dic);
            DC.Parameters.Add(dic);

            //
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.None;
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
            if (XConfig.IsDebug
                && DC.Parameters.Count > 0
                && list[0].ID == 1)
            {
                lock (XDebug.Lock)
                {
                    XDebug.Dics = FlatDics(DC.Parameters);
                    XDebug.SetValue();
                }
            }

            //
            return paras;
        }

        /*******************************************************************************************************/

        internal DicParam CharLengthDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.ColumnOneAlias = cp.Key;
            dic.TableAliasOne = cp.Alias;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Format = format;

            return dic;
        }

        internal DicParam CompareDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = cp.ValType;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;

            return dic;
        }
        internal DicParam NotInDic(ColumnParam cp, ValueInfo v)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ColumnOne = "OneEqualOne";
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
            dic.ClassFullName = cp.ClassFullName;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.TableAliasOne = cp.Alias;
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
            dic.ClassFullName = cp.ClassFullName;
            dic.TableAliasOne = cp.Alias;
            dic.PropOne = cp.Prop;
            dic.ColumnOne = cp.Key;
            dic.ColumnOneAlias = colAlias;

            return dic;
        }

        internal DicParam DistinctDic()
        {
            var dic = SetDicBase(DC);

            return dic;
        }

        internal DicParam CountDic(string fullName, string key, string alias = "")
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }
        internal DicParam SumDic(string fullName, string key, string alias = "")
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicParam OrderbyDic(string fullName, string key, string alias)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam InsertDic(string fullName, List<DicParam> list)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.Inserts = list;

            return dic;
        }
        internal DicParam InsertHelperDic(string fullName, string key, ValueInfo v, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = v?.Val;
            dic.CsValueStr = v?.ValStr;
            dic.CsType = valType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam SetDic(string fullName, string key, string param, ValueInfo v, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
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
            dic.ClassFullName = cp1.ClassFullName;
            dic.ColumnOne = cp1.Key;
            dic.TableAliasOne = cp1.Alias;
            dic.ColumnTwo = cp2.Key;
            dic.TableAliasTwo = cp2.Alias;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam TableDic(string fullName, string alias)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            return dic;
        }

        internal DicParam ColumnDic(ColumnParam cp)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = cp.ClassFullName;
            dic.TableAliasOne = cp.Alias;
            dic.ColumnOne = cp.Key;
            dic.Param = cp.Key;
            dic.ParamRaw = cp.Key;
            dic.PropOne = cp.Prop;

            return dic;
        }
        internal DicParam ColumnDic(string columnOne, string tableAliasOne, string fullName, string prop)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.PropOne = prop;
            dic.ColumnOne = columnOne;
            dic.TableAliasOne = tableAliasOne;

            return dic;
        }

        internal DicParam JoinColumnDic(string fullName, string key, string alias, string prop)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.PropOne = prop;
            dic.ColumnOne = key;
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

        internal DicParam GetNewDic()
        {
            return SetDicBase(DC);
        }

        internal DicParam SelectColumnDic(List<DicParam> cols)
        {
            var dic = SetDicBase(DC);
            dic.Columns = cols;
            return dic;
        }

    }
}
