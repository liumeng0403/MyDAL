using System;

namespace HPC.DAL
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
    }
}
