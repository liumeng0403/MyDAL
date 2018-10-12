using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Yunyong.Core;

namespace Yunyong.DataExchange.Core.Helper
{
    internal class GenericHelper : ClassInstance<GenericHelper>
    {
        public object GetTypeValue(PropertyInfo outerProp, object outerObj)
        {
            return outerProp.GetValue(outerObj);
        }
        public object GetTypeValue(object objVal)
        {
            return objVal;
        }

        public List<PropertyInfo> GetPropertyInfos(Type mType)
        {
            var props = mType.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            return props;
        }

        public Assembly LoadAssembly(string fullClassName)
        {


            if (string.IsNullOrWhiteSpace(fullClassName))
            {
                return null;
            }

            //
            var ass = fullClassName.Substring(0, fullClassName.LastIndexOf('.'));
            var assD = $"{ass}.dll";
            var assE = $"{ass}.exe";
            var assemD = default(Assembly);
            var assemE = default(Assembly);

            //
            try
            {
                assemD = Assembly.LoadFrom(assD);
            }
            catch
            {
                assemD = LoadAssembly(ass);
            }
            if (assemD == null)
            {
                try
                {
                    assemE = Assembly.LoadFrom(assE);
                }
                catch
                {
                    assemE = LoadAssembly(ass);
                }
            }

            //
            if (assemD != null)
            {
                return assemD;
            }
            else if (assemE != null)
            {
                return assemE;
            }
            else
            {
                return null;
            }
        }

        internal static T ConvertT<T>(object value)
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
