using MyDAL.Core.Bases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class GenericHelper
    {

        private Context DC { get; set; }
        internal GenericHelper(Context dc)
        {
            DC = dc;
        }
        
        /*******************************************************************************************************************/

        internal object GetObjPropValue(PropertyInfo outerProp, object outerObj)
        {
            return outerProp.GetValue(outerObj);
        }
        internal List<PropertyInfo> GetPropertyInfos(Type mType)
        {
            var props = mType.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            return props;
        }

        internal T ConvertT<T>(object value)
        {
            //
            if (value == null || value is DBNull)
            {
                return default(T);
            }

            //
            if (value is T)
            {
                return (T)value;
            }

            //
            var type = typeof(T);
            type = Nullable.GetUnderlyingType(type) ?? type;

            //
            if (type.IsEnum)
            {
                if (value is float || value is double || value is decimal)
                {
                    value = Convert.ChangeType(value, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
                }
                return (T)Enum.ToObject(type, value);
            }

            //
            return (T)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }
    }
}
