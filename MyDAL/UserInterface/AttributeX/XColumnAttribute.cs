using System;

namespace MyDAL
{
    [AttributeUsage( AttributeTargets.Property,AllowMultiple =false,Inherited =false)]
    public sealed class XColumnAttribute : Attribute
    {
        public XColumnAttribute(string columnName)
        {
            Name = columnName;
        }

        /// <summary>
        /// DB 列名
        /// </summary>
        public string Name { get; set; } = string.Empty;        
    }
}
