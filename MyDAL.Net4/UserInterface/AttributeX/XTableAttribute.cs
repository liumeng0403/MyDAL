using System;

namespace MyDAL
{
    /// <summary>
    /// 用于标记与 DB Table 对应的实体类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class XTableAttribute 
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public XTableAttribute() { }

        /// <summary>
        /// DB - 表名
        /// </summary>
        public string Name { get; set; }
    }
}
