using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Models.MysqlFunctionParam.DicParamModel;
using MyDAL.Core.表达式能力;

namespace MyDAL.Core.Models.MysqlFunctionParam.DicParamResolve
{
    internal class CountResolve
    {
        internal CountParam Resolve(Context DC,MethodCallExpression mcExpr)
        {
            var type = mcExpr.Method.DeclaringType;
            
            DC.Option = OptionEnum.ColumnAs;
            DC.Compare = CompareXEnum.None;
            var cp = new hql_获取列().GetKey(DC,mcExpr, FuncEnum.Count, CompareXEnum.None);
            DC.Func = FuncEnum.Count;
            var format = DC.TSH.DateTime(cp.Format);
            CountParam param = CountDic(DC, DC.TbM1, cp.Prop,string.Empty);
            param.FuncName = "COUNT";
            return param;
            
            
            // if (type == XConfig.CSTC.String
            //     || type.IsEnum)
            // {
            //     return DC.XE.MemberAccessHandle(mcExpr.Object as MemberExpression);
            // }
            // else if (type.IsSimpleValueType())
            // {
            //     return SimpleValueTypeToString(mcExpr);
            // }
            // else if (type.IsNullable())
            // {
            //     var typeT = Nullable.GetUnderlyingType(type);
            //     if (typeT.IsEnum)
            //     {
            //         return DC.XE.MemberAccessHandle(mcExpr.Object as MemberExpression);
            //     }
            //     else if(typeT.IsSimpleValueType())
            //     {
            //         return SimpleValueTypeToString(mcExpr);
            //     }
            //     else
            //     {
            //         return null;
            //     }
            // }
            // else if (type == XConfig.CSTC.ByteArray)
            // {
            //     throw XConfig.EC.Exception(XConfig.EC._093, $"【byte[]】对应 DB column 不能使用 C# .ToString() 函数！表达式--【{mcExpr.ToString()}】");
            // }
            // else
            // {
            //     DC.Option = OptionEnum.ColumnAs;
            //     DC.Compare = CompareXEnum.None;
            //     var cp = DC.XE.GetKey(mcExpr, FuncEnum.ToString_CS_DateTime_Format, CompareXEnum.None);
            //     DC.Func = FuncEnum.ToString_CS_DateTime_Format;
            //     var format = DC.TSH.DateTime(cp.Format);
            //     if (DC.Action == ActionEnum.Select)
            //     {
            //         return DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.DateFormatDic(cp, null, format) });
            //     }
            //     else
            //     {
            //         return DC.DPH.DateFormatDic(cp, null, format);
            //     }
            // }
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DC">上下文</param>
        /// <param name="mType">表实体类型</param>
        /// <param name="key">字段属性名</param>
        /// <param name="alias">表别名</param>
        /// <returns></returns>
        internal CountParam CountDic(Context DC,Type mType, string key, string alias = "")
        {
            CountParam dic = new CountParam();
            dic.SetDicBase(DC);
            dic.TbMType = mType;
            dic.TbAlias = alias;
            dic.TbCol = dic.GetCol(DC,mType, key);  // key;
            dic.Param = key;
            dic.ParamRaw = key;

            return dic;
        }
    }
}