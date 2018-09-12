using System;
using System.Collections.Generic;
using System.Text;

namespace Yunyong.DataExchange.Common
{
    public class ColumnInfo
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string ColumnDefault { get; set; }
        public string IsNullable { get; set; }
        public string ColumnComment { get; set; }
    }
}
