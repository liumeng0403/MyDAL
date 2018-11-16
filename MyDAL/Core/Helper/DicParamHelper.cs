using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.Core.Helper
{
    internal class DicParamHelper
    {
        private static DicParam SetDicBase( Context dc)
        {
            return new DicParam
            {
                //
                ID = 0,
                IsDbSet=false,
                Crud = dc.Crud,
                Action = dc.Action,
                Option = dc.Option,
                Compare = dc.Compare,
                Func = dc.Func,

                //
                GroupAction= ActionEnum.None,
                GroupRef=null
            };
        }
        private static DicParam InHelper(DicParam ui, object csVal, string csValStr)
        {
            var cp = new DicParam
            {
                //
                ID = ui.ID,
                IsDbSet=false,
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
                TvpIndex = ui.TvpIndex,
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
            if (DC.IsInParameter(dic))
            {
                InNotIn(dic);
            }
            else if (dic.Group != null)
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
        }
        private void DbParam(DicParam ui, DicParam refDb)
        {
            if(ui.IsDbSet)
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
                ui.Key = DC.SC.GetModelKey(ui.ClassFullName);
                ui.TableOne = DC.SC.GetModelTableName(ui.Key);
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
            var paras = new DbParamInfo();

            //
            foreach (var db in list)
            {
                if (DC.IsParameter(db.Action))
                {
                    if (db.Group != null)
                    {
                        paras.Add(GetParameters(db.Group));
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

        internal DicParam CharLengthDic(string fullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }

        internal DicParam TrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }
        internal DicParam LTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }
        internal DicParam RTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }
        internal DicParam DateFormatDic(string key, string alias, (object val, string valStr) value, Type valType,string format)
        {
            var dic = SetDicBase(DC);
            dic.ColumnOne = key;
            dic.ColumnOneAlias = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;
            dic.Format = format;

            return dic;
        }

        internal DicParam CompareDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicParam InDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }
        internal DicParam NotInDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicParam LikeDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicParam OneEqualOneDic((object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ColumnOne = "OneEqualOne";
            dic.Param = "OneEqualOne";
            dic.ParamRaw = "OneEqualOne";
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }

        internal DicParam IsNullDic(string classFullName, string key, string alias, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = null;
            dic.CsValueStr = string.Empty;
            dic.CsType = valType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam SelectMemberInitDic(string fullName, string key, string alias, string colAlias)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.ColumnOneAlias = colAlias;

            return dic;
        }

        internal DicParam DistinctDic()
        {
            var dic = SetDicBase(DC);

            return dic;
        }

        internal DicParam ColumnDic(string columnOne, string tableAliasOne, string fullName)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = columnOne;
            dic.TableAliasOne = tableAliasOne;

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

        internal DicParam InsertDic(string fullName, string key, (object val, string valStr) val, Type valType, int tvpIdx)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = val.val;
            dic.CsValueStr = val.valStr;
            dic.CsType = valType;
            dic.TvpIndex = tvpIdx;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam SetDic(string fullName, string key, string param, (object val, string valStr) val, Type valType)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = param;
            dic.ParamRaw = param;
            dic.CsValue = val.val;
            dic.CsValueStr = val.valStr;
            dic.CsType = valType;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicParam OnDic(string fullName, string key1, string alias1, string key2, string alias2)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key1;
            dic.TableAliasOne = alias1;
            dic.ColumnTwo = key2;
            dic.TableAliasTwo = alias2;

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

        internal DicParam ColumnDic(string fullName, string key)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;

            return dic;
        }

        internal DicParam JoinColumnDic(string fullName, string key, string alias)
        {
            var dic = SetDicBase(DC);
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
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

    }
}
