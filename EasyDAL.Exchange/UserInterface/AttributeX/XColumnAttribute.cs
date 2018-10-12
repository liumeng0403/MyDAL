using System;

namespace Yunyong.DataExchange
{
    public class XColumnAttribute : Attribute
    {
        /// <summary>
        /// DB 列名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// DB key 类型
        /// </summary>
        public KeyTypeEnum Key { get; set; } = KeyTypeEnum.None;

        /// <summary>
        /// DB 数据类型
        /// </summary>
        public DataTypeEnum DataType { get; set; } = DataTypeEnum.None;

        /// <summary>
        /// DB 自增?
        /// </summary>
        public bool AutoIncrement { get; set; } = false;

        /// <summary>
        /// DB 允许空?
        /// </summary>
        public bool AllowNull { get; set; } = true;

        /// <summary>
        /// DB 默认值
        /// </summary>
        public object Default { get; set; } = null;

        /// <summary>
        /// DB 长度
        /// </summary>
        public long Length { get; set; } = -1;

        /// <summary>
        /// DB 精度
        /// </summary>
        public short Precision { get; set; } = -1;

    }
}
