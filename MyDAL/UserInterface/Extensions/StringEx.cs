namespace HPC.DAL.ModelTools
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

    }
}
