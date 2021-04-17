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
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Is not null/empty/whitespace ?
        /// </summary>
        public static bool IsNotNullStr(this string str)
        {
            return !str.IsNullStr();
        }

        public static bool ToBool(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            return str.ToBool(false);
        }

        public static bool ToBool(this IReadOnlyCollection<string> str, bool defaultValueIfNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            //
            if (null == str
                || str.Count == 0)
            {
                return defaultValueIfNull;
            }

            var strValue = str.ToString();

            if (strValue.IsNullStr())
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

        public static int ToInt(this IReadOnlyCollection<string> str)
        {


            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */


            try
            {
                if (null == str
                    || str.Count == 0)
                {
                    throw XConfig.EC.Exception(XConfig.EC._112, $".ToInt() 的对象为 null 或 无值 !!!");
                }

                return Convert.ToInt32(str.ToString());
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._143, $"int ToInt(this IReadOnlyCollection<string> str) -- {str?.ToString()}，InnerExeception：{ex.Message}");
            }
        }
        
        public static int ToInt(this IReadOnlyCollection<string> str, int defaultValueIfNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (null == str
                || str.Count == 0)
            {
                return defaultValueIfNull;
            }

            return str.ToInt();
        }
        
        public static int? ToIntNull(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (null == str
                || str.Count == 0)
            {
                return null;
            }

            return str.ToInt();
        }

        public static long ToLong(this IReadOnlyCollection<string> str)
        {


            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */


            try
            {
                if (null == str
                    || str.Count == 0)
                {
                    throw XConfig.EC.Exception(XConfig.EC._146, $"long ToLong(this IReadOnlyCollection<string> str) 的对象为 null 或 无值 !!!");
                }

                return Convert.ToInt64(str.ToString());
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._150, $"long ToLong(this IReadOnlyCollection<string> str) -- {str?.ToString()}，InnerExeception：{ex.Message}");
            }
        }

        public static long ToLong(this IReadOnlyCollection<string> str, long defaultValueIfNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (null == str
                || str.Count == 0)
            {
                return defaultValueIfNull;
            }

            return str.ToLong();
        }

        public static long? ToLongNull(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (null == str
                || str.Count == 0)
            {
                return null;
            }

            return str.ToLong();
        }

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
