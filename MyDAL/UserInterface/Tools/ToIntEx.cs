using MyDAL.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Tools
{
    public static class ToIntEx
    {

        public static bool IsInt(this IReadOnlyCollection<string> str)
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

            return int.TryParse(str.ToString(), out var result);
        }

        public static bool IsNotInt(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            return !str.IsInt();
        }

        public static int ToInt(this IReadOnlyCollection<string> str)
        {


            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */


            try
            {
                if (str.IsNotInt())
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

        public static int ToInt(this IReadOnlyCollection<string> str, int defaultVauleWhenNull)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (str.IsNotInt())
            {
                return defaultVauleWhenNull;
            }

            return str.ToInt();
        }

        public static int? ToIntNull(this IReadOnlyCollection<string> str)
        {

            /*
             * 如:
             * Microsoft.Extensions.Primitives.StringValues
             */

            if (str.IsNotInt())
            {
                return null;
            }

            return str.ToInt();
        }



        public static int ToInt(this object obj)
        {
            try
            {
                if (null == obj)
                {
                    throw XConfig.EC.Exception(XConfig.EC._113, $".ToInt() 的对象为 null !!!");
                }
                else if (obj is string)
                {
                    return Convert.ToInt32(obj);
                }
                else if (obj is IEnumerable)     //  如  Microsoft.Extensions.Primitives.StringValues
                {
                    return obj.ToString().ToInt();
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._064, $"int ToInt(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
        }
        public static int ToInt(this object obj, int customValue)
        {
            try
            {
                return obj.ToInt();
            }
            catch
            {
                return customValue;
            }
        }
        public static int? ToIntNull(this object obj)
        {
            try
            {
                return obj.ToInt();
            }
            catch
            {
                return null;
            }
        }

    }
}
