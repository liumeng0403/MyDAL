using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core.ExpressionX
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

        internal static DicModelUI BinaryCharLengthHandle(string key, string alias, object value, Type valType, ExpressionType nodeType, bool isR)
        {
            return new DicModelUI
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = value,
                CsType = valType,
                Option = OptionEnum.CharLength,
                Compare = GetOption(nodeType, isR)
            };
        }
        // 01
        internal static DicModelUI BinaryCompareHandle(CrudTypeEnum crud, ActionEnum action, string classFullName, string key, string alias, object value, Type valType, CompareEnum compare)
        {
            return new DicModelUI
            {
                ClassFullName = classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                CsType = valType,
                Param = key,
                ParamRaw = key,
                Crud = crud,
                Action = action,
                Option = OptionEnum.Compare,
                Compare = compare
            };
        }
        // 01
        internal static DicModelUI CallInHandle(CrudTypeEnum crud, ActionEnum action, string classFullName, string key, string alias, object value, Type valType)
        {
            return new DicModelUI
            {
                ClassFullName = classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                CsType = valType,
                Param = key,
                ParamRaw = key,
                Crud = crud,
                Action = action,
                Option = OptionEnum.In
            };
        }
        // 01
        internal static DicModelUI CallLikeHandle(CrudTypeEnum crud, ActionEnum action, string classFullName, string key, string alias, object value, Type valType)
        {
            return new DicModelUI
            {
                ClassFullName = classFullName,
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                CsType = valType,
                Param = key,
                ParamRaw = key,
                Crud = crud,
                Action = action,
                Option = OptionEnum.Like
            };
        }
        // 01
        internal static DicModelUI ConstantBoolHandle(object value, Type valType)
        {
            return new DicModelUI
            {
                ColumnOne = "OneEqualOne",
                Param = "OneEqualOne",
                ParamRaw = "OneEqualOne",
                CsValue = value,
                CsType = valType,
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
                CsType = valType,
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

        internal static DicModelUI ConditionCountHandle(CrudTypeEnum crud, string key, string alias = "")
        {
            return new DicModelUI
            {
                TableAliasOne = alias,
                ColumnOne = key,
                Param = key,
                ParamRaw = key,
                Action = ActionEnum.Select,
                Option = OptionEnum.Count,
                Crud = crud
            };
        }

    }
}
