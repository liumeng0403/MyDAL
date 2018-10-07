using MyDAL.Common;
using MyDAL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyDAL.Helper
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
        [Obsolete("废弃方法,仅作参考用!")]
        public string GetTypeValue(Type valType, PropertyInfo outerProp, object outerObj)
        {
            var val = string.Empty;

            if (valType == typeof(sbyte))
            {
                val = ((sbyte)outerProp.GetValue(outerObj, null)).ToString();
            }
            else if (valType == typeof(byte))
            {
                val = ((byte)outerProp.GetValue(outerObj, null)).ToString();
            }
            else if (valType == typeof(char))
            {
                val = ((char)outerProp.GetValue(outerObj, null)).ToString();
            }
            else if (valType == typeof(bool))
            {
                val = ((bool)outerProp.GetValue(outerObj, null)).ToString();
            }
            else if (valType == typeof(short)
                || valType == typeof(int)
                || valType == typeof(long))
            {
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            else if (valType == typeof(ushort)
                || valType == typeof(uint)
                || valType == typeof(ulong))
            {
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            else if (valType == typeof(float)
                || valType == typeof(decimal)
                || valType == typeof(double))
            {
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            else if (valType == typeof(DateTime))
            {
                val = outerProp.GetValue(outerObj, null).ToDatetimeStr();
            }
            else if (valType == typeof(Guid))
            {
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            else if (valType == typeof(string))
            {
                val = outerProp.GetValue(outerObj, null)?.ToString();
            }
            else if (valType == typeof(sbyte?)
                || valType == typeof(byte?)
                || valType == typeof(char?)
                || valType == typeof(bool?)
                || valType == typeof(short?)
                || valType == typeof(int?)
                || valType == typeof(long?)
                || valType == typeof(ushort?)
                || valType == typeof(uint?)
                || valType == typeof(ulong?)
                || valType == typeof(float?)
                || valType == typeof(decimal?)
                || valType == typeof(double?)
                || valType == typeof(DateTime?)
                || valType == typeof(Guid?))
            {
                var obj = outerProp.GetValue(outerObj, null);
                if (obj == null)
                {
                    val = null;
                }
                else
                {
                    if (valType == typeof(DateTime?))
                    {
                        val = obj.ToDatetimeStr();
                    }
                    else
                    {
                        val = obj.ToString();
                    }
                }
            }
            else if (valType.IsEnum)
            {
                //val = ((int)outerProp.GetValue(outerObj, null)).ToString();
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            else
            {
                val = outerProp.GetValue(outerObj, null).ToString();
            }
            return val;
        }
        [Obsolete("废弃方法,仅作参考用!")]
        public string GetTypeValue(Type valType, object objVal)
        {
            var val = string.Empty;

            if (valType == typeof(sbyte))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(short))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(int))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(long))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(byte))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(ushort))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(uint))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(ulong))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(float))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(double))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(decimal))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(bool))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(char))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(DateTime))
            {
                val = objVal.ToDatetimeStr();
            }
            else if (valType == typeof(Guid))
            {
                val = objVal.ToString();
            }
            else if (valType == typeof(string))
            {
                val = objVal.ToString();
            }
            else if (valType.IsEnum)
            {
                //val = ((int)objVal).ToString();
                val = objVal.ToString();
            }
            else
            {
                val = objVal.ToString();
            }
            return val;
        }

        public List<PropertyInfo> GetPropertyInfos<M>(M m)
        {
            if (m == null)
            {
                return new List<PropertyInfo>();
            }
            var props = m.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            return props;
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

    }
}
