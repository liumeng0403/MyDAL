using System;

namespace MyDAL
{
    /// <summary>
    /// 用于 Paging Query 的查询
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XQueryAttribute 
        : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public XQueryAttribute() { }

        /// <summary>
        /// TableModel - 类型
        /// </summary>
        public Type Table { get; set; } = default(Type);

        /// <summary>
        /// Table - 列名
        /// </summary>
        public string Column { get; set; } = string.Empty;

        /// <summary>
        /// Where - 比较条件
        /// </summary>
        public CompareEnum Compare { get; set; } = CompareEnum.None;

    }
}
