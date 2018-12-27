using System;

namespace MyDAL
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class XTableAttribute 
        : Attribute
    {
        public XTableAttribute() { }

        /// <summary>
        /// DB - 表名
        /// </summary>
        public string Name { get; set; }
    }
}
