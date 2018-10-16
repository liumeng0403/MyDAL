using System;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core.ExpressionX
{
    internal class DicHandle
        : ClassInstance<DicHandle>
    {
        internal Context DC { get; set; }

        internal static DicModelUI UiDicCopy(DicModelUI ui, object csVal, OptionEnum option)
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
                CsType = ui.CsType,
                TvpIndex = ui.TvpIndex
            };

            //
            cp.CsValue = csVal;
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

        internal DicModelUI CharLengthDic(string key, string alias, object value, Type valType)//, CompareEnum compare)// ExpressionType nodeType, bool isR)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value;
            dic.CsType = valType;

            //dic.Option = OptionEnum.CharLength,
            //dic.Compare = compare // GetCompareType(nodeType, isR)

            return dic;

            //return new DicModelUI
            //{
            //    ColumnOne = key,
            //    TableAliasOne = alias,
            //    Param = key,
            //    ParamRaw = key,
            //    CsValue = value,
            //    CsType = valType,
            //    Option = OptionEnum.CharLength,
            //    Compare = compare // GetCompareType(nodeType, isR)
            //};
        }

        internal DicModelUI TrimDic(string key, string alias, object value, Type valType)//, CompareEnum compare) // ExpressionType nodeType, bool isR)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value;
            dic.CsType = valType;

            //dic.Option = OptionEnum.Trim;
            //dic.Compare = compare; // GetCompareType(nodeType, isR)

            return dic;

            //return new DicModelUI
            //{
            //    ColumnOne = key,
            //    TableAliasOne = alias,
            //    Param = key,
            //    ParamRaw = key,
            //    CsValue = value,
            //    CsType = valType,
            //    Option = OptionEnum.Trim,
            //    Compare = compare // GetCompareType(nodeType, isR)
            //};
        }
        internal DicModelUI LTrimDic(string key, string alias, object value, Type valType)//, CompareEnum compare)  // ExpressionType nodeType, bool isR)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value;
            dic.CsType = valType;

            //dic.Option = OptionEnum.LTrim,
            //dic.Compare = compare // GetCompareType(nodeType, isR)

            return dic;
            //return new DicModelUI
            //{
            //    ColumnOne = key,
            //    TableAliasOne = alias,
            //    Param = key,
            //    ParamRaw = key,
            //    CsValue = value,
            //    CsType = valType,
            //    Option = OptionEnum.LTrim,
            //    Compare = compare // GetCompareType(nodeType, isR)
            //};
        }
        internal DicModelUI RTrimDic(string key, string alias, object value, Type valType)//, CompareEnum compare)// ExpressionType nodeType, bool isR)
        {
            var dic = SetDicBase();
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = value;
            dic.CsType = valType;

            //dic.Option = OptionEnum.RTrim;
            //dic.Compare = compare;  // GetCompareType(nodeType, isR)

            return dic;

            //return new DicModelUI
            //{
            //    ColumnOne = key,
            //    TableAliasOne = alias,
            //    Param = key,
            //    ParamRaw = key,
            //    CsValue = value,
            //    CsType = valType,
            //    Option = OptionEnum.RTrim,
            //    Compare = compare  // GetCompareType(nodeType, isR)
            //};
        }

        internal DicModelUI CompareDic(string classFullName, string key, string alias, object value, Type valType)//, CompareEnum compare)
        {
            var dic = SetDicBase();
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicModelUI InDic(string classFullName, string key, string alias, object value, Type valType)
        {
            var dic = SetDicBase();
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicModelUI LikeDic(string classFullName, string key, string alias, object value, Type valType)
        {
            var dic = SetDicBase();
            dic.ClassFullName = classFullName;
            dic.ColumnOne = key;
            dic.TableAliasOne = alias;
            dic.CsValue = value;
            dic.CsType = valType;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }

        internal DicModelUI OneEqualOneDic(object value, Type valType)
        {
            var dic = SetDicBase();
            dic.ColumnOne = "OneEqualOne";
            dic.Param = "OneEqualOne";
            dic.ParamRaw = "OneEqualOne";
            dic.CsValue = value;
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
            dic.CsType = valType;

            //dic.Option = option,  // optionx,
            //dic.Compare = CompareEnum.None

            return dic;
            //return new DicModelUI
            //{
            //    ClassFullName = classFullName, // tuple.classFullName,
            //    ColumnOne = key,  // tuple.key,
            //    TableAliasOne = alias,  // tuple.alias,
            //    Param = key,  // tuple.key,
            //    ParamRaw = key,  // tuple.key,
            //    CsValue = null,
            //    CsType = valType,  // tuple.valType,
            //    Option = option,  // optionx,
            //    Compare = CompareEnum.None
            //};
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
            //dic.Option = OptionEnum.Count;


            return dic;

            //return new DicModelUI
            //{
            //    ClassFullName = fullName,
            //    TableAliasOne = alias,
            //    ColumnOne = key,
            //    Param = key,
            //    ParamRaw = key,
            //    Action = ActionEnum.Select,
            //    Option = OptionEnum.Count,

            //};
        }

        internal DicModelUI OrderbyDic(string fullName, string key)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            
            return dic;
        }

        /*******************************************************************************************************/

        internal DicModelUI InsertDic(string fullName, string key, object val, Type valType, OptionEnum option, int tvpIdx)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = key;
            dic.ParamRaw = key;
            dic.CsValue = val;
            dic.CsType = valType;
            dic.TvpIndex = tvpIdx;


            dic.Action = ActionEnum.Insert;
            //dic.Option = option,

            return dic;

            //return new DicModelUI
            //{
            //    ClassFullName = fullName,
            //    ColumnOne = key, // prop.Name,
            //    Param = key, // prop.Name,
            //    ParamRaw = key, // prop.Name,
            //    CsValue = val,
            //    CsType = valType, // prop.PropertyType,
            //    Action = ActionEnum.Insert,
            //    Option = option,
            //    TvpIndex = tvpIdx, // index
            //};
        }

        /*******************************************************************************************************/

        internal DicModelUI SetDic(string fullName, string key, string param, object val, Type valType, ActionEnum action)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key;
            dic.Param = param;
            dic.ParamRaw = param;
            dic.CsValue = val;
            dic.CsType = valType;

            //Option = option,
            dic.Action = action;
            //Crud = CrudTypeEnum.Update

            return dic;

            //return new DicModelUI
            //{
            //    ClassFullName = fullName, // typeof(M).FullName,
            //    ColumnOne = key,
            //    Param = param, // key,
            //    ParamRaw = param, // key,
            //    CsValue = val,
            //    CsType = valType,  // typeof(F),
            //    Option = option,
            //    Action = action,
            //    Crud = CrudTypeEnum.Update
            //};

            //new DicModelUI
            //{
            //    ClassFullName = fullName,
            //    ColumnOne =key, // tp.key,
            //    Param = param, // tp.param,
            //    ParamRaw = param,  // tp.param,
            //    CsValue = val, // tp.val,
            //    CsType = valType,  // tp.valType,
            //    Option = option,  // OptionEnum.Set,
            //    Action = action,  // ActionEnum.Update,
            //    Crud = CrudTypeEnum.Update
            //}
        }

        /*******************************************************************************************************/

        internal DicModelUI OnDic(string fullName, string key1, string alias1, string key2, string alias2)//, OptionEnum option, CompareEnum compare)
        {
            var dic = SetDicBase();
            dic.ClassFullName = fullName;
            dic.ColumnOne = key1;
            dic.TableAliasOne = alias1;
            dic.ColumnTwo = key2;
            dic.TableAliasTwo = alias2;

            //dic.Option = option,
            //dic.Compare = compare  // GetCompareType(binExpr.NodeType, false)

            return dic;

            //return new DicModelUI
            //{
            //    ClassFullName = fullName,// tuple1.classFullName,
            //    ColumnOne = key1,// tuple1.key,
            //    TableAliasOne = alias1, // tuple1.alias,
            //    ColumnTwo = key2, // tuple2.key,
            //    TableAliasTwo = alias2,  // tuple2.alias,
            //    Option = option,
            //    Compare = compare  // GetCompareType(binExpr.NodeType, false)
            //};
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
