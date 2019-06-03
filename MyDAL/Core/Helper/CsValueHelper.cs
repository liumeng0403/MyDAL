using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Tools;
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

        private string InValue(object vals)
        {
            var sb = new StringBuilder();
            foreach (var item in (vals as IEnumerable))
            {
                sb.Append(item); sb.Append(',');
            }
            if (sb.Length <= 0)
            {
                throw XConfig.EC.Exception(XConfig.EC._077, " SQL 中 in() 的条件个数不能为 0 !!!");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
        private string DateTimeProcess(object val, string format)
        {
            return val.ToDateTimeStr(format);
        }
        internal string ValueProcess(object val, Type valType, string format)
        {
            if (valType == XConfig.CSTC.DateTime)
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
            //
            var val = Expression.Lambda(expr).Compile().DynamicInvoke();
            if (expr != null
                && val == null)
            {
                throw XConfig.EC.Exception(XConfig.EC._078, $"[[{expr.ToString()}]] 中,传入的 SQL 筛选条件为 Null !!!");
            }

            //
            var type = val.GetType();
            if (type.IsArray
                || type.IsEnumerable()
                || type.IsList())
            {
                val = InValue(val);
            }

            //
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

    }
}
