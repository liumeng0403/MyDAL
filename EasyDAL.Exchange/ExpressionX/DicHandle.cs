using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.ExpressionX
{
    internal static class DicHandle
    {

        // 02
        internal static OptionEnum GetOption(ExpressionType nodeType)
        {
            var option = OptionEnum.None;
            if (nodeType == ExpressionType.Equal)
            {
                option = OptionEnum.Equal;
            }
            else if (nodeType == ExpressionType.NotEqual)
            {
                option = OptionEnum.NotEqual;
            }
            else if (nodeType == ExpressionType.LessThan)
            {
                option = OptionEnum.LessThan;
            }
            else if (nodeType == ExpressionType.LessThanOrEqual)
            {
                option = OptionEnum.LessThanOrEqual;
            }
            else if (nodeType == ExpressionType.GreaterThan)
            {
                option = OptionEnum.GreaterThan;
            }
            else if (nodeType == ExpressionType.GreaterThanOrEqual)
            {
                option = OptionEnum.GreaterThanOrEqual;
            }

            return option;
        }

        /*******************************************************************************************************/

        internal static DicModel BinaryCharLengthHandle(string key, string value, Type valType, ExpressionType nodeType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.CharLength,
                FuncSupplement = GetOption(nodeType)
            };
        }
        // 01
        internal static DicModel BinaryNormalHandle(string key, string value, Type valType, ExpressionType nodeType)
        {
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = GetOption(nodeType)
            };
        }
        // 01
        internal static DicModel CallInHandle(string key, string value, Type valType)
        {
            if (valType.IsEnum)
            {
                valType = typeof(int);
            }
            return new DicModel
            {
                KeyOne = key,
                Param = key,
                ParamRaw = key,
                Value = value,
                ValueType = valType,
                Option = OptionEnum.In
            };
        }
        // 01
        internal static DicModel CallLikeHandle(string key, string value, Type valType)
        {
            return new DicModel
            {
                KeyOne = key,
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
                KeyOne = "OneEqualOne",
                Param = "OneEqualOne",
                ParamRaw = "OneEqualOne",
                Value = value,
                ValueType = valType,
                Option = OptionEnum.OneEqualOne
            };
        }
        // 01
        internal static DicModel MemberBoolHandle(string key, Type valType)
        {
            return new DicModel
            {
                KeyOne = key,
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
