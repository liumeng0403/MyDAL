using System;

namespace MyDAL
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public sealed class XTableAttribute:Attribute
    {
        public XTableAttribute(string tableName)
        {
            Name = tableName;
        }

        /// <summary>
        /// DB 表名
        /// </summary>
        public string Name { get; }
    }
}
