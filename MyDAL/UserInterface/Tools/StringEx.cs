using System;

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
        [Obsolete("警告：建议使用 Tools 命名空间中此方法， 此 API 后面会从 ModelTools 中移除！！！", false)]
        public static bool IsNullStr(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

    }
}
namespace HPC.DAL.Tools
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
