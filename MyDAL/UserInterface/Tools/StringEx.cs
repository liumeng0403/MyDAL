using MyDAL.Core;
using System;
using System.Collections.Generic;

namespace MyDAL.Tools
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class StringEx
    {

        // datetime? tostring 方法

        // 自然 sql 命名 , select insert update delete 

        // create 返回 实体 , 支持自增 主键 携带返回      
        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        [Obsolete("请使用IsEmpty/IsBlank方法")]
        public static bool IsNullStr(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Is null/empty ?
        /// </summary>
        public static bool IsEmpty(this string str)
        {
            return null == str || 0==str.Length;
        }
        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsBlank(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Is not null/empty/whitespace ?
        /// </summary>
        [Obsolete("请使用IsNotEmpty/IsNotBlank方法")]
        public static bool IsNotNullStr(this string str)
        {
            return !str.IsNullStr();
        }

        /// <summary>
        /// Is not null/empty/whitespace ?
        /// </summary>
        public static bool IsNotEmpty(this string str)
        {
            return !str.IsEmpty();
        }
        /// <summary>
        /// Is not null/empty/whitespace ?
        /// </summary>
        public static bool IsNotBlank(this string str)
        {
            return !str.IsBlank();
        }

        public static bool ToBool(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            return str.ToBool(false);
        }

        public static bool ToBool(this IReadOnlyCollection<string> str, bool defaultVauleWhenNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            //
            if (null == str
                || str.Count == 0)
            {
                return defaultVauleWhenNull;
            }

            var strValue = str.ToString();

            if (strValue.IsBlank())
            {
                return false;
            }
            else
            {
                strValue = strValue.Trim().ToLower();
                if ("0".Equals(strValue)
                    || "false".Equals(strValue))
                {
                    return false;
                }
                else if ("1".Equals(strValue)
                        || "true".Equals(strValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool? ToBoolNull(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            //
            if (null == str
                || str.Count == 0)
            {
                return null;
            }

            return str.ToBool();
        }

        //public static bool? ToBoolNull(this IReadOnlyCollection<string> str, bool defaultValueIfNull){ }    // 此方法无比要定义

        // ----------------------------------------------------------------------------------------------------------------------

        // -----------------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Obj is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this object obj)
        {
            if (null == obj)
            {
                return true;
            }
            else
            {
                return obj.ToString().IsNullStr();
            }
        }

        /// <summary>
        /// Obj is not null/empty/whitespace ?
        /// </summary>
        public static bool IsNotNullStr(this object obj)
        {
            return !obj.IsNullStr();
        }

        public static DateTime ToDateTime(this string str)
        {
            try
            {
                if (str.IsNullStr())
                {
                    throw XConfig.EC.Exception(XConfig.EC._115, $"DateTime ToDateTime(this object obj) -- 参数 str 为 null !!!");
                }
                return Convert.ToDateTime(str);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._114, $"DateTime ToDateTime(this object obj) -- {str?.ToString()}，InnerExeception：{ex.Message}");
            }
        }

    }
}
