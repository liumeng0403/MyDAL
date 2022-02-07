using System.Diagnostics.CodeAnalysis;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name = "auto_increment_demo")]
    public class AutoIncrementDemo
    {
        [XColumn(Name = "id",IsPK = true,IsPkAutoIncrement = true)]
        public long? id { get; set; }
        
        [XColumn(Name = "col_1")]
        public string col1 { get; set; } 
        
        [XColumn(Name = "col_2")]
        public string col2 { get; set; }
    }
}