using MyDAL.Test.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="platformmonthlyperformance")]
    public class PlatformMonthlyPerformance 
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public int Year { get; set; }
        
        public Month Month { get; set; }

        public decimal PerformanceAmount { get; set; }
    }
}
