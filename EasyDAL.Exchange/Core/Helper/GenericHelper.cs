using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.Core.Helper
{
    internal class GenericHelper
    {

        private Context DC { get; set; }

        internal GenericHelper(Context dc)
        {
            DC = dc;
        }

        /*******************************************************************************************************************/

        private Assembly LoadAssemblyR(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            //
            var ass = name.Substring(0, name.LastIndexOf('.'));
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
                assemD = LoadAssemblyR(ass);
            }
            if (assemD == null)
            {
                try
                {
                    assemE = Assembly.LoadFrom(assE);
                }
                catch
                {
                    assemE = LoadAssemblyR(ass);
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

        /*******************************************************************************************************************/

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

        public Assembly LoadAssembly(string fullName)
        {

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return null;
            }

            //
            var assD = $"{fullName}.dll";
            var assE = $"{fullName}.exe";
            var assemD = default(Assembly);
            var assemE = default(Assembly);

            //
            try
            {
                assemD = Assembly.LoadFrom(assD);
            }
            catch
            {
                assemD = LoadAssemblyR(fullName);
            }
            if (assemD == null)
            {
                try
                {
                    assemE = Assembly.LoadFrom(assE);
                }
                catch
                {
                    assemE = LoadAssemblyR(fullName);
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
