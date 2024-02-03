using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Core.表达式能力;

namespace MyDAL.Core.Expressions
{
    internal sealed class CsToStringExpression
    {
        private Context DC { get; }

        private CsToStringExpression() { }
        internal CsToStringExpression(Context dc)
        {
            DC = dc;
        }

        private DicParam SimpleValueTypeToString(MethodCallExpression mcExpr)
        {
            DC.Option = OptionEnum.ColumnAs;
            DC.Compare = CompareXEnum.None;
            var cp = new hql_获取列().GetKey(DC,mcExpr, ColFuncEnum.ToString_CS, CompareXEnum.None);
            return DC.DPH.SelectColumnDic(new List<DicParam>
            {
                DC.DPH.CsToStringDic(cp, null)
            },ColFuncEnum.ToString_CS);
        }

        internal DicParam WhereFuncToString(Expression left, BinExprInfo bin,ColFuncEnum colFunc)
        {
            var cp = new hql_获取列().GetKey(DC,left, ColFuncEnum.ToString_CS_DateTime_Format, CompareXEnum.None);
            var val = DC.VH.ValueProcess(bin.Right, cp.ValType, cp.Format);
            DC.Option = OptionEnum.Function;
            DC.Compare = bin.Compare;
            
            var format = DC.TSH.DateTime(cp.Format);
            return DC.DPH.DateFormatDic(cp, val, format,ColFuncEnum.ToString_CS_DateTime_Format);
        }
        internal DicParam SelectFuncToString(MethodCallExpression mcExpr,ColFuncEnum colFunc)
        {
            var type = mcExpr.Object.Type;
            if (type == XConfig.CSTC.String
                 || type.IsEnum)
            {
                return DC.XE.MemberAccessHandle(mcExpr.Object as MemberExpression,colFunc);
            }
            else if (type.IsSimpleValueType())
            {
                return SimpleValueTypeToString(mcExpr);
            }
            else if (type.IsNullable())
            {
                var typeT = Nullable.GetUnderlyingType(type);
                if (typeT.IsEnum)
                {
                    return DC.XE.MemberAccessHandle(mcExpr.Object as MemberExpression,colFunc);
                }
                else if(typeT.IsSimpleValueType())
                {
                    return SimpleValueTypeToString(mcExpr);
                }
                else
                {
                    return null;
                }
            }
            else if (type == XConfig.CSTC.ByteArray)
            {
                throw XConfig.EC.Exception(XConfig.EC._093, $"【byte[]】对应 DB column 不能使用 C# .ToString() 函数！表达式--【{mcExpr.ToString()}】");
            }
            else
            {
                DC.Option = OptionEnum.ColumnAs;
                DC.Compare = CompareXEnum.None;
                var cp = new hql_获取列().GetKey(DC,mcExpr, ColFuncEnum.ToString_CS_DateTime_Format, CompareXEnum.None);
                var format = DC.TSH.DateTime(cp.Format);
                if (DC.Action == ActionEnum.Select)
                {
                    return DC.DPH.SelectColumnDic(new List<DicParam>
                    {
                        DC.DPH.DateFormatDic(cp, null, format,ColFuncEnum.ToString_CS_DateTime_Format)
                    },ColFuncEnum.ToString_CS_DateTime_Format);
                }
                else
                {
                    return DC.DPH.DateFormatDic(cp, null, format,ColFuncEnum.ToString_CS_DateTime_Format);
                }
            }
        }
    }
}
