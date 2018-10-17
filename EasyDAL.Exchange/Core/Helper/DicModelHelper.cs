using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;

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

        internal static DicModelUI UiDicCopy(DicModelUI ui, object csVal,string csValStr, OptionEnum option)
        {
            var cp = new DicModelUI
            {
                //
                ID = ui.ID,
                Crud = ui.Crud,
                Action = ui.Action,
                Option = ui.Option,
                Compare = ui.Compare,

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
                CsValueStr=ui.CsValueStr,
                CsType = ui.CsType,
                TvpIndex = ui.TvpIndex
            };

            //
            cp.CsValue = csVal;
            cp.CsValueStr = csValStr;
            cp.Option = option;

            //
            return cp;
        }

        private DicModelUI SetDicBase()
        {
            return new DicModelUI
            {
                ID = 0,
                Crud = DC.Crud,
                Action = DC.Action,
                Option = DC.Option,
                Compare = DC.Compare
            };
        }

        /*******************************************************************************************************/

        internal DicModelUI CharLengthDic(string key, string alias, (object val, string valStr) value, Type valType)
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

        internal DicModelUI TrimDic(string key, string alias, (object val, string valStr) value, Type valType)
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
        internal DicModelUI LTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
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
        internal DicModelUI RTrimDic(string key, string alias, (object val, string valStr) value, Type valType)
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

        internal DicModelUI CompareDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
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

        internal DicModelUI InDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
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

        internal DicModelUI LikeDic(string classFullName, string key, string alias, (object val, string valStr) value, Type valType)
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

        internal DicModelUI OneEqualOneDic((object val, string valStr) value, Type valType)
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

        internal DicModelUI IsNullDic(string classFullName, string key, string alias, Type valType)
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

        internal DicModelUI SelectMemberInitDic(string fullName, string key, string alias, string colAlias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.ColumnOneAlias = colAlias;

            return dic;
        }

        internal static DicModelUI ColumnDic(string columnOne, string tableAliasOne, string fullName)
        {
            return new DicModelUI
            {
                ClassFullName = fullName,
                ColumnOne = columnOne,
                TableAliasOne = tableAliasOne,
                Action = ActionEnum.Select,
                Option = OptionEnum.Column,
                Crud = CrudTypeEnum.Join
            };
        }

        internal DicModelUI CountDic(string fullName, string key, string alias = "")
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;

            dic.Action = ActionEnum.Select;

            return dic;
        }

        internal DicModelUI OrderbyDic(string fullName, string key)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            
            return dic;
        }

        /*******************************************************************************************************/

        internal DicModelUI InsertDic(string fullName, string key, (object val, string valStr) val, Type valType, OptionEnum option, int tvpIdx)
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
            
            dic.Action = ActionEnum.Insert;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicModelUI SetDic(string fullName, string key, string param, (object val, string valStr) val, Type valType, ActionEnum action)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = param;
            dic.ParamRaw = param;
            dic.CsValue = val.val;
            dic.CsValueStr = val.valStr;
            dic.CsType = valType;
            
            dic.Action = action;

            return dic;
        }

        /*******************************************************************************************************/

        internal DicModelUI OnDic(string fullName, string key1, string alias1, string key2, string alias2)
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

        internal DicModelUI TableDic(string fullName, string alias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            return dic;
        }

        internal DicModelUI ColumnDic(string fullName, string key)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;

            return dic;
        }

        internal DicModelUI JoinColumnDic(string fullName, string key, string alias)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.TableAliasOne = alias;
            dic.ColumnOne = key;
            return dic;
        }

    }
}
