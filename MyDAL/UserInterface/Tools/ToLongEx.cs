using MyDAL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Tools
{
    public static class ToLongEx
    {


        public static bool IsLong(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (null == str
                || str.Count == 0)
            {
                return false;
            }

            return long.TryParse(str.ToString(), out var result);
        }
        public static bool IsNotLong(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            return !str.IsLong();
        }
        public static long ToLong(this IReadOnlyCollection<string> str)
        {


            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (str.IsNotLong())
            {
                throw XConfig.EC.Exception(XConfig.EC._146, $"long ToLong(this IReadOnlyCollection<string> str) 的对象为 null 或 无值 !!!");
            }
            else
            {
                return Convert.ToInt64(str.ToString());
            }
        }
        public static long ToLong(this IReadOnlyCollection<string> str, long defaultVauleWhenNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (str.IsNotLong())
            {
                return defaultVauleWhenNull;
            }
            else
            {
                return str.ToLong();
            }
        }
        public static long? ToLongNull(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (str.IsNotLong())
            {
                return null;
            }
            else
            {
                return str.ToLong();
            }
        }

        public static bool IsLong(this string str)
        {
            if (str.IsBlank())
            {
                return false;
            }

            return long.TryParse(str, out var result);
        }
        public static bool IsNotLong(this string str)
        {
            return !str.IsLong();
        }
        public static long ToLong(this string str)
        {
            if (str.IsLong())
            {
                return Convert.ToInt64(str);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._065, $"long ToLong(this object obj) -- {str}，InnerExeception: 非数字字符串转换!");
            }
        }
        public static long ToLong(this string str, long defaultVauleWhenNull)
        {
            if (str.IsLong())
            {
                return str.ToLong();
            }
            else
            {
                return defaultVauleWhenNull;
            }
        }
        public static long? ToLongNull(this string str)
        {
            if (str.IsLong())
            {
                return str.ToLong();
            }
            else
            {
                return null;
            }
        }

        public static bool IsLong(this long? number)
        {
            if (number.IsNull())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsNotLong(this long? number)
        {
            return !number.IsLong();
        }
        public static long ToLong(this long? number)
        {
            if (number.IsLong())
            {
                return number.Value;
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._132, $"long ToLong(this long? number) -- 转换目标为null值!");
            }
        }
        public static long ToLong(this long? number, long defaultVauleWhenNull)
        {
            if (number.IsLong())
            {
                return number.Value;
            }
            else
            {
                return defaultVauleWhenNull;
            }
        }
        // public static long? ToLongNull(this long? number){  }   //  不需要此方法


    }
}
