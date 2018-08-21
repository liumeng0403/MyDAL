using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Attributes
{
    /// <summary>
    /// DB 表名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">DB 表名</param>
        public TableAttribute(string tableName)
        {
            Name = tableName;
        }

        /// <summary>
        /// DB 表名
        /// </summary>
        public string Name { get; set; }
    }
}
