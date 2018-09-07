using EasyDAL.Exchange.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Helper
{
    internal class AttributeHelper: ClassInstance<AttributeHelper>
    {
        /// <summary>
        /// Cache Data
        /// </summary>
        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

        /// <summary>
        /// 缓存Collection Name Key
        /// </summary>
        private string BuildKey(Type type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return type.FullName;
            }
            return $"{type.FullName}.{name}";
        }

        private string GetValue<T>(Type type, Func<T, string> attributeValueFunc, string name)
            where T : Attribute
        {
            object attribute = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }
            else
            {
                var propertyInfo = type.GetProperty(name);
                if (propertyInfo != null)
                {
                    attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }
                var fieldInfo = type.GetField(name);
                if (fieldInfo != null)
                {
                    attribute = fieldInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                }
            }
            return attribute == null ?
                string.Empty :
                attributeValueFunc((T)attribute);
        }

        private string GetAttributeValue< A>(Type type, Func<A, string> attributeValueFunc, string name)
            where A : Attribute
        {
            var key = BuildKey(type, name);
            if (!Cache.ContainsKey(key))
            {
                //CacheAttributeValue(type, attributeValueFunc, name);
                //var keyx = BuildKey(type, name);
                var value = GetValue(type, attributeValueFunc, name);
                lock ($"{key}_attributeValueLockKey")
                {
                    if (!Cache.ContainsKey(key))
                    {
                        Cache[key] = value;
                    }
                }
            }
            return Cache[key];
        }

        /************************************************************************************************************************************/

        public string GetPropertyValue<M, A>(M m, Func<A, string> attributeValueFunc, string name)
            where A : Attribute
        {
            return GetAttributeValue(m.GetType(), attributeValueFunc, name);
        }

        public string GetPropertyValue< A>(Type type, Func<A, string> attributeValueFunc)
            where A : Attribute
        {
            return GetAttributeValue(type, attributeValueFunc, null);
        }

        public Attribute GetAttribute<M,A>(M m, PropertyInfo prop)
        {
            try
            {
                return m
                    .GetType()
                    .GetMember(prop.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)[0]
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:" + ex.Message);
            }
        }
    }
}
