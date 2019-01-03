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
        public Type Table { get; set; } = default(Type);

        /// <summary>
        /// Table - 列名
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Where - 比较条件
        /// </summary>
        public CompareEnum Compare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LogicEnum Logic { get; set; } = LogicEnum.And;
    }
}
