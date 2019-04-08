using HPC.DAL;
using MyDAL.Test.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{

    /*
     * CREATE TABLE `platformmonthlyperformance` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `Year` int(11) NOT NULL,
     * `Month` int(11) NOT NULL COMMENT '月份',
     * `PerformanceAmount` decimal(65,30) NOT NULL COMMENT '金额',
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
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
