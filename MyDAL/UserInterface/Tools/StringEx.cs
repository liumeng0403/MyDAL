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
        
        /// <summary>
        /// 是 null 或 "" 字符串 ?
        /// </summary>
        public static bool IsEmpty(this string str)
        {
            return null == str || 0 == str.Length;
        }

        /// <summary>
        /// 不是 null 或 "" 字符串 ?
        /// </summary>
        public static bool IsNotEmpty(this string str)
        {
            return !str.IsEmpty();
        }

        /// <summary>
        /// 是 null 或 "" 或 空白 字符串？
        /// </summary>
        public static bool IsBlank(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 不是 null 或 "" 或 空白 字符串？
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

        public static DateTime ToDateTime(this string str)
        {
            try
            {
                if (str.IsBlank())
                {
                    throw XConfig.EC.Exception(XConfig.EC._115,
                        $"DateTime ToDateTime(this object obj) -- 参数 str 为 null !!!");
                }

                return Convert.ToDateTime(str);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._114,
                    $"DateTime ToDateTime(this object obj) -- {str?.ToString()}，InnerExeception：{ex.Message}");
            }
        }

        /// <summary>
        /// 是否相同(不区分大小写)？
        /// </summary>
        public static bool EqualsIgnoreCase(this string str, string someStr)
        {
            if (null == str || null == someStr)
            {
                return false;
            }

            return str.Equals(someStr, StringComparison.OrdinalIgnoreCase);
        }
    }
}