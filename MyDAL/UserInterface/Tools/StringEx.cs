using MyDAL.Core;
using System;

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
