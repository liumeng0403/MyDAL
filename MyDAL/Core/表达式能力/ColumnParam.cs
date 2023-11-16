using System;

namespace MyDAL.Core.表达式能力
{
    /// <summary>
    /// 列信息
    /// </summary>
    internal class ColumnParam
    {
        /// <summary>
        /// 属性名
        /// </summary>
        internal string Prop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal string Alias { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        internal Type ValType { get; set; }
        /// <summary>
        /// 表模型类型
        /// </summary>
        internal Type TbMType { get; set; }
        internal string Format { get; set; }
    }
}
