using MyDAL.Common;
using MyDAL.Enums;
using System;
using System.Linq.Expressions;

namespace MyDAL.ExpressionX
{
    internal static class DicHandle
    {

        // 02
        internal static CompareEnum GetOption(ExpressionType nodeType, bool isR)
        {
            var option = CompareEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = !isR ? CompareEnum.Equal : CompareEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = !isR ? CompareEnum.NotEqual : CompareEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = !isR ? CompareEnum.LessThan : CompareEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = !isR ? CompareEnum.LessThanOrEqual : CompareEnum.GreaterThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = !isR ? CompareEnum.GreaterThan : CompareEnum.LessThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = !isR ? CompareEnum.GreaterThanOrEqual : CompareEnum.LessThanOrEqual;
            }

            return option;
        }

        /*******************************************************************************************************/

        internal static DicModelUI BinaryCharLengthHandle(string key, string alias, string value, Type valType, ExpressionType nodeType, bool isR)
        {
            return new DicModelUI
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = value,
                ValueType = valType,
                Option = OptionEnum.CharLength,
                Compare = GetOption(nodeType, isR)
            };
        }
        // 01
        internal static DicModelUI BinaryCompareHandle(CrudTypeEnum crud, ActionEnum action,string classFullName,string key, string alias, string value, Type valType,CompareEnum compare)
        {
            return new DicModelUI
            {
                ClassFullName=classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                ValueType = valType,
                Param = key,
                ParamRaw = key,
                Crud= crud,
                Action = action,
                Option = OptionEnum.Compare,
                Compare = compare
            };
        }
        // 01
        internal static DicModelUI CallInHandle(CrudTypeEnum crud,ActionEnum action, string classFullName, string key, string alias, string value, Type valType)
        {
            return new DicModelUI
            {
                ClassFullName = classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                ValueType = valType,
                Param = key,
                ParamRaw = key,
                Crud = crud,
                Action = action,
                Option = OptionEnum.In
            };
        }
        // 01
        internal static DicModelUI CallLikeHandle(CrudTypeEnum crud, ActionEnum action, string classFullName, string key, string alias, string value, Type valType)
        {
            return new DicModelUI
            {
                ClassFullName = classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                ValueType = valType,
                Param = key,
                ParamRaw = key,
                Crud = crud,
                Action = action,
                Option = OptionEnum.Like
            };
        }
        // 01
        internal static DicModelUI ConstantBoolHandle(string value, Type valType)
        {
            return new DicModelUI
            {
                ColumnOne = "OneEqualOne",
                Param = "OneEqualOne",
                ParamRaw = "OneEqualOne",
                CsValue = value,
                ValueType = valType,
                Option = OptionEnum.OneEqualOne
            };
        }
        // 01
        internal static DicModelUI MemberBoolHandle(string key, string alias, Type valType)
        {
            return new DicModelUI
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = true.ToString(),
                ValueType = valType,
                Option = OptionEnum.Compare,
                Compare = CompareEnum.Equal
            };
        }

        /*******************************************************************************************************/

        internal static DicModelUI SelectColumnHandle(string columnOne, string tableAliasOne)
        {
            return new DicModelUI
            {
                ColumnOne = columnOne,
                TableAliasOne = tableAliasOne,
                Action = ActionEnum.Select,
                Option = OptionEnum.Column,
                Crud = CrudTypeEnum.Join
            };
        }

        /*******************************************************************************************************/

        internal static DicModelUI ConditionCountHandle(string key)
        {
            return new DicModelUI
            {
                ColumnOne = key,
                Param = key,
                ParamRaw = key,
                Action = ActionEnum.Select,
                Option = OptionEnum.Count,
                Crud = CrudTypeEnum.Query
            };
        }

    }
}
