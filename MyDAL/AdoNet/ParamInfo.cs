using System.Data;

namespace MyDAL.AdoNet
{
    /// <summary>
    /// SQL 参数
    /// </summary>
    internal sealed class ParamInfo
    {
        /// <summary>
        /// 参数 -- 名称
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 参数 -- 值
        /// </summary>
        internal object Value { get; set; }

        /// <summary>
        /// 参数 -- 方向(in or out)
        /// </summary>
        internal ParameterDirection ParameterDirection { get; set; }

        /// <summary>
        /// 参数 -- 类型
        /// </summary>
        internal DbType Type { get; set; }

        /// <summary>
        /// 参数 -- 大小(单位字节)
        /// </summary>
        internal int? Size { get; set; }

        /// <summary>
        /// 参数 -- 精度(最大位数)
        /// </summary>
        internal byte? Precision { get; set; }

        /// <summary>
        /// 参数 -- 小数位数
        /// </summary>
        internal byte? Scale { get; set; }
    }
}
