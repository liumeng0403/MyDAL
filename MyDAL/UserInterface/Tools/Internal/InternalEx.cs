using MyDAL.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Tools
{
    public static class InternalEx
    {

        internal static long ToLong(this object obj)
        {
            try
            {
                if (obj is IEnumerable)    //   如 Microsoft.Extensions.Primitives.StringValues
                {
                    return Convert.ToInt64(obj.ToString());
                }
                return Convert.ToInt64(obj);
            }
            catch (Exception ex)
            {
                throw XConfig.EC.Exception(XConfig.EC._065, $"long ToLong(this object obj) -- {obj?.ToString()}，InnerExeception：{ex.Message}");
            }
        }
        internal static long ToLong(this object obj, long customValue)
        {
            try
            {
                return obj.ToLong();
            }
            catch
            {
                return customValue;
            }
        }
        internal static long? ToLongNull(this object obj)
        {
            try
            {
                if (obj.IsNullStr())
                {
                    return null;
                }
                return obj.ToLong();
            }
            catch
            {
                return null;
            }
        }

    }
}
