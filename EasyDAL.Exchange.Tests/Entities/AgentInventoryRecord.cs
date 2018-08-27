using EasyDAL.Exchange.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities
{
    [Table(Name = "AgentInventoryRecord")]
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
