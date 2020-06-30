using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Core.Common
{
    internal class ColumnInfo
    {
        internal string TableName { get; set; }
        internal string ColumnName { get; set; }
        internal string DataType { get; set; }
        internal string ColumnDefault { get; set; }
        internal string IsNullable { get; set; }
        internal string ColumnComment { get; set; }
        internal string KeyType { get; set; }
    }
}
