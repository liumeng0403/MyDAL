using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Yunyong.DataExchange.ExpressionX
{
    internal static class DicHandle
    {

        // 02
        internal static OptionEnum GetOption(ExpressionType nodeType, bool isR)
        {
            var option = OptionEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = !isR ? OptionEnum.Equal : OptionEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = !isR ? OptionEnum.NotEqual : OptionEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = !isR ? OptionEnum.LessThan : OptionEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = !isR ? OptionEnum.LessThanOrEqual : OptionEnum.GreaterThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = !isR ? OptionEnum.GreaterThan : OptionEnum.LessThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = !isR ? OptionEnum.GreaterThanOrEqual : OptionEnum.LessThanOrEqual;
            }

            return option;
        }

        /*******************************************************************************************************/

        internal static DicModel BinaryCharLengthHandle(string key,string alias, string value, Type valType, ExpressionType nodeType,bool isR)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.CharLength,
                FuncSupplement = GetOption(nodeType,isR)
            };
        }
        // 01
        internal static DicModel BinaryNormalHandle(string key,string alias, string value, Type valType, ExpressionType nodeType,bool isR)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Value = value,
                ValueType = valType,
                Param = key,
                ParamRaw = key,
                Option = GetOption(nodeType,isR)
            };
        }
        // 01
        internal static DicModel CallInHandle(string key,string alias, string value, Type valType)
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
                Value = value,
                ValueType = valType,
                Option = OptionEnum.In
            };
        }
        // 01
        internal static DicModel CallLikeHandle(string key,string alias, string value, Type valType)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                Value = value,
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
                Value = value,
                ValueType = valType,
                Option = OptionEnum.OneEqualOne
            };
        }
        // 01
        internal static DicModel MemberBoolHandle(string key,string alias, Type valType)
        {
            return new DicModel
            {
                ColumnOne = key,
                TableAliasOne = alias,
                Param = key,
                ParamRaw = key,
                Value = true.ToString(),
                ValueType = valType,
                Option = OptionEnum.Equal
            };
        }

        /*******************************************************************************************************/

    }
}
