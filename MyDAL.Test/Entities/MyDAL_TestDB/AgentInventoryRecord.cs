
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    /*
     * CREATE TABLE `agentinventoryrecord` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `AgentId` char(36) NOT NULL,
     * `ProductId` char(36) NOT NULL,
     * `StockingCount` int(11) NOT NULL,
     * `TotalStockingCount` int(11) NOT NULL,
     * `UnconfirmedStockingCount` int(11) NOT NULL,
     * `TotalSaleCount` int(11) NOT NULL,
     * `UnconfirmedSaleCount` int(11) NOT NULL,
     * `LockedCount` int(11) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table("AgentInventoryRecord")]
    public class AgentInventoryRecord 
    {

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid AgentId { get; set; }

        public Guid ProductId { get; set; }
        
        public int StockingCount { get; set; }
        
        public int TotalStockingCount { get; set; }
        
        public int UnconfirmedStockingCount { get; set; }
        
        public int TotalSaleCount { get; set; }
        
        public int UnconfirmedSaleCount { get; set; }
        
        public int LockedCount { get; set; }
    }
}
