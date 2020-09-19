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

    }
}
