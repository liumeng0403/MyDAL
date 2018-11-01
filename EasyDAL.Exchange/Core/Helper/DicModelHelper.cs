using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL.Core.Helper
{
    internal class DicModelHelper
    {
        internal Context DC { get; set; }

        internal DicModelHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

        internal static DicUI UiDicCopy(DicUI ui, object csVal, string csValStr, OptionEnum option)
        {
            var cp = new DicUI
            {
                //
                ID = ui.ID,
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
            cp.Option = option;

            //
            return cp;
        }
        private void Copy(DicUI ui, List<DicDB> dbList, DicDB refDb)
        {
            var db = new DicDB();

            //
            db.ID = ui.ID;
            db.Crud = ui.Crud;
            db.Action = ui.Action;
            db.Option = ui.Option;
            db.Compare = ui.Compare;
            db.Func = ui.Func;

            //
            if (ui.ClassFullName.IsNullStr())
            {
                db.Key = string.Empty;
            }
            else
            {
                db.Key = DC.SC.GetModelKey(ui.ClassFullName);
                db.TableOne = DC.SC.GetModelTableName(db.Key);
            }
            db.TableAliasOne = ui.TableAliasOne;
            db.ColumnOne = ui.ColumnOne;
            db.KeyTwo = ui.ColumnTwo;
            db.AliasTwo = ui.TableAliasTwo;
            db.ColumnAlias = ui.ColumnOneAlias;
            db.Param = ui.Param;
            db.ParamRaw = ui.ParamRaw;
            db.TvpIndex = ui.TvpIndex;
            if (ui.CsType != null)
            {
                if (DC.IsInParameter(ui.CsValue, ui.Option))
                {
                    db.ParamInfo = ParameterHelper.GetDefault(ui.Param, ui.CsValue, DbType.String);
                }
                else
                {
                    DC.PH.GetDbVal(ui, db, ui.CsType);
                }
            }
            if (ui.Group != null)
            {
                db.Group = new List<DicDB>();
                db.GroupAction = ui.GroupAction;
                foreach (var item in ui.Group)
                {
                    Copy(item, db.Group, db);
                }
                dbList.Add(db);
                return;
            }
            db.GroupRef = refDb;
            if (ui.InItems != null)
            {
                db.InItems = new List<DicDB>();
                foreach (var u in ui.InItems)
                {
                    Copy(u, db.InItems, refDb);
                }
            }
            dbList.Add(db);
        }
        internal void UiToDbCopy()
        {
            if (DC.UiConditions != null)
            {
                foreach (var ui in DC.UiConditions)
                {
                    if (DC.DbConditions.Any(dm => dm.ID == ui.ID))
                    {
                        continue;
                    }
                    Copy(ui, DC.DbConditions, null);
                }
            }
        }

        private void InNotInDicProcess(DicUI dic, List<DicUI> list)
        {
            var vals = dic.CsValue.ToString().Split(',').Select(it => it);
            foreach (var val in vals)
            {
                var dicx = UiDicCopy(dic, val, dic.CsValueStr, OptionEnum.InHelper);
                UniqueDicContext(dicx, list);
                list.Add(dicx);
            }
        }
        internal void UniqueDicContext(DicUI dic, List<DicUI> list)
        {
            //
            dic.ID = DC.DicID;
            DC.DicID++;
            if (!dic.ParamRaw.IsNullStr())
            {
                dic.Param = $"{dic.ParamRaw}__{dic.ID}";
            }

            //
            if (DC.IsInParameter(dic.CsValue, dic.Option))
            {
                InNotInDicProcess(dic, list);
            }
            else if (dic.Group != null)
            {
                foreach (var ui in dic.Group)
                {
                    if (DC.IsInParameter(ui.CsValue, ui.Option))
                    {
                        ui.InItems = new List<DicUI>();
                        UniqueDicContext(ui, ui.InItems);
                    }
                    else
                    {
                        UniqueDicContext(ui, dic.Group);
                    }
                    ui.GroupRef = dic;
                }
            }
        }

        private DicUI SetDicBase()
        {
            return new DicUI
            {
                ID = 0,
                Crud = DC.Crud,
                Action = DC.Action,
                Option = DC.Option,
                Compare = DC.Compare,
                Func = DC.Func
            };
        }

        /*******************************************************************************************************/

        internal DicUI CharLengthDic(string fullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI TrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }
        internal DicUI LTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }
        internal DicUI RTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }

        internal DicUI CompareDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI InDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
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
        internal DicUI NotInDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI LikeDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI OneEqualOneDic((object val, string valStr) value, Type valType)
        {
            var dic = SetDicBase();
            dic.ColumnOne = "OneEqualOne";
            dic.Param = "OneEqualOne";
            dic.ParamRaw = "OneEqualOne";
            dic.CsValue = value.val;
            dic.CsValueStr = value.valStr;
            dic.CsType = valType;

            return dic;
        }

        internal DicUI IsNullDic(string classFullName, string key, string alias, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI SelectMemberInitDic(string fullName, string key, string alias, string colAlias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.ColumnOneAlias = colAlias;

            return dic;
        }

        internal DicUI ColumnDic(string columnOne, string tableAliasOne, string fullName)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = columnOne;
            dic.TableAliasOne = tableAliasOne;

            return dic;
        }

        internal DicUI CountDic(string fullName, string key, string alias = "")
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicUI OrderbyDic(string fullName, string key, string alias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicUI InsertDic(string fullName, string key, (object val, string valStr) val, Type valType, int tvpIdx)
        {
            var dic = SetDicBase();
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

        internal DicUI SetDic(string fullName, string key, string param, (object val, string valStr) val, Type valType)
        {
            var dic = SetDicBase();
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

        internal DicUI OnDic(string fullName, string key1, string alias1, string key2, string alias2)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key1;
            dic.TableAliasOne = alias1;
            dic.ColumnTwo = key2;
            dic.TableAliasTwo = alias2;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicUI TableDic(string fullName, string alias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            return dic;
        }

        internal DicUI ColumnDic(string fullName, string key)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;

            return dic;
        }

        internal DicUI JoinColumnDic(string fullName, string key, string alias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            return dic;
        }

        /*******************************************************************************************************/

        internal DicUI GroupDic(ActionEnum action)
        {
            var dic = SetDicBase();
            dic.Group = new List<DicUI>();
            dic.GroupAction = action;
            return dic;
        }

    }
}
