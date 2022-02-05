using System;

namespace MyDAL
{
    /// <summary>
    /// 用于标记 DB-Table 的 M 或 VM 的属性, 以指示属性对应的 DB-Column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XColumnAttribute
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public XColumnAttribute() { }

        /// <summary>
        /// Table - 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPK { get; set; } = false;

        /// <summary>
        /// 是否主键自增
        /// </summary>
        public bool IsPkAutoIncrement { get; set; } = false;
    }
}
