using System;

namespace MyDAL
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XQueryAttribute 
        : Attribute
    {
        public XQueryAttribute() { }

        /// <summary>
        /// TableModel - 类型
        /// </summary>
        public Type Table { get; set; }

        /// <summary>
        /// Table - 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Where - 比较条件
        /// </summary>
        public CompareEnum Compare { get; set; }
    }
}
