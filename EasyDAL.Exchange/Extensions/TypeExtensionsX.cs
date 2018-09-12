using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Yunyong.DataExchange.Extensions
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

    }
}
