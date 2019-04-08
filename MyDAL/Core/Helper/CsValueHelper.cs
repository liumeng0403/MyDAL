using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Extensions;
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace HPC.DAL.Core.Helper
{
    internal class CsValueHelper
    {

        private Context DC { get; set; }
        private CsValueHelper() { }
        internal CsValueHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************/

        private string InValue(object vals, bool isArray)
        {
            //var ds = vals as dynamic;
            var ds = vals as IList;
            if (ds.Count <= 0)
            {
                throw new Exception(" SQL 中 in() 的条件个数不能为 0 !!!");
            }
            var sb = new StringBuilder();
            for (var i = 0; i < ds.Count; i++)
            {
                sb.Append(ds[i]);
                if (i != ds.Count - 1)
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }
        private string DateTimeProcess(object val, string format)
        {
            return val.ToDateTimeStr(format);
        }
        internal string ValueProcess(object val, Type valType, string format)
        {
            if (valType == XConfig.TC.DateTime)
            {
                return DateTimeProcess(val, format);
            }
            else if (valType.IsNullable())
            {
                return ValueProcess(val, Nullable.GetUnderlyingType(valType), format);
            }
            return string.Empty;
        }

        /*******************************************************************************************************/

        internal ValueInfo ValueProcess(Expression expr, Type valType, string format = "")
        {
            var val = Expression.Lambda(expr).Compile().DynamicInvoke();
            if (expr != null
                && val == null)
            {
                throw new Exception($"[[{expr.ToString()}]] 中,传入的 SQL 筛选条件为 Null !!!");
            }
            var type = val.GetType();
            if (type.IsArray)
            {
                val = InValue(val, true);
            }
            else if (type.IsList())
            {
                val = InValue(val, false);
            }
            return new ValueInfo
            {
                Val = val,
                ValStr = ValueProcess(val, valType, format)
            };
        }
        internal ValueInfo PropertyValue(PropertyInfo prop, object obj)
        {
            var valtype = prop.PropertyType;
            var val = DC.GH.GetObjPropValue(prop, obj);
            var valstr = ValueProcess(val, valtype, string.Empty);
            return new ValueInfo
            {
                Val = val,
                ValStr = valstr
            };
        }
        internal ValueInfo ExpandoObjectValue(object obj)
        {
            var valType = obj.GetType();
            var val = obj;
            var valStr = ValueProcess(val, valType, string.Empty);
            return new ValueInfo
            {
                Val = val,
                ValStr = valStr
            };
        }
        internal (string val, Type valType) InValue(Type type, object vals)
        {
            var isArray = false;
            var valType = default(Type);
            var typeT = default(Type);
            if (type.IsArray)
            {
                typeT = type.GetElementType();
                isArray = true;
            }
            else
            {
                typeT = type.GetGenericArguments()[0];
            }
            if (typeT.IsNullable())
            {
                typeT = Nullable.GetUnderlyingType(typeT);
            }
            valType = typeT;
            return (InValue(vals, isArray), valType);
        }

    }
}
