using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange.ExpressionX
{
    internal static class DicHandle
    {

        // 02
        internal static CompareConditionEnum GetOption(ExpressionType nodeType, bool isR)
        {
            var option = CompareConditionEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = !isR ? CompareConditionEnum.Equal : CompareConditionEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = !isR ? CompareConditionEnum.NotEqual : CompareConditionEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = !isR ? CompareConditionEnum.LessThan : CompareConditionEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = !isR ? CompareConditionEnum.LessThanOrEqual : CompareConditionEnum.GreaterThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = !isR ? CompareConditionEnum.GreaterThan : CompareConditionEnum.LessThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = !isR ? CompareConditionEnum.GreaterThanOrEqual : CompareConditionEnum.LessThanOrEqual;
            }

            return option;
        }

        /*******************************************************************************************************/

        internal static DicModel BinaryCharLengthHandle(string key, string alias, string value, Type valType, ExpressionType nodeType, bool isR)
        {
            return new DicModel
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
        internal static DicModel BinaryNormalHandle(string key, string alias, string value, Type valType, ExpressionType nodeType, bool isR)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                CsValue = value,
                ValueType = valType,
                Param = key,
                ParamRaw = key,
                Option = OptionEnum.Compare,
                Compare = GetOption(nodeType, isR)
            };
        }
        // 01
        internal static DicModel CallInHandle(string key, string alias, string value, Type valType)
        {
            if (valType.IsEnum)
            {
                valType = typeof(int);
            }
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = value,
                ValueType = valType,
                Option = OptionEnum.In
            };
        }
        // 01
        internal static DicModel CallLikeHandle(string key, string alias, string value, Type valType)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = value,
                ValueType = valType,
                Option = OptionEnum.Like
            };
        }
        // 01
        internal static DicModel ConstantBoolHandle(string value, Type valType)
        {
            return new DicModel
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
        internal static DicModel MemberBoolHandle(string key, string alias, Type valType)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                CsValue = true.ToString(),
                ValueType = valType,
                Option = OptionEnum.Compare,
                Compare = CompareConditionEnum.Equal
            };
        }

        /*******************************************************************************************************/

        internal static DicModel SelectColumnHandle(string columnOne, string tableAliasOne)
        {
            return new DicModel
            {
                ColumnOne = columnOne,
                TableAliasOne = tableAliasOne,
                Action = ActionEnum.Select,
                Option = OptionEnum.Column,
                Crud = CrudTypeEnum.Join
            };
        }

    }
}
