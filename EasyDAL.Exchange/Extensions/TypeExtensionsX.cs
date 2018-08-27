using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtensionsX
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueTypeX(this Type type) => type.IsValueType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumX(this Type type) => type.IsEnum;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeCode GetTypeCodeX(Type type) => Type.GetTypeCode(type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static MethodInfo GetPublicInstanceMethodX(this Type type, string name, Type[] types)
        {
            return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, types, null);
        }
    }
}
