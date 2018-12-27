using System;

namespace MyDAL
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XColumnAttribute 
        : Attribute
    {
        public XColumnAttribute() { }

        /// <summary>
        /// Table - 列名
        /// </summary>
        public string Name { get; set; }
    }
}
