using System;

namespace HPC.DAL
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
        /// DB-Table 对应的 Model 类型
        /// </summary>
        public Type Table { get; set; } = default(Type);

        /// <summary>
        /// 表别名 - 在连接查询时,如:自连接查询,因为 Table-Type 相同,这时需要指定别名...
        /// </summary>
        public string TableAlias { get; set; } = string.Empty;

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
