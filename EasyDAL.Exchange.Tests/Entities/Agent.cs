using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities
{
    /*
     * CREATE TABLE `agent` (
     * `Id` char(36) NOT NULL,
     * `CreatedOn` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
     * `UserId` char(36) NOT NULL,
     * `PathId` longtext,
     * `Name` longtext NOT NULL,
     * `Phone` longtext NOT NULL,
     * `IdCardNo` longtext,
     * `CrmUserId` longtext,
     * `AgentLevel` int(11) NOT NULL,
     * `ActivedOn` datetime(6) DEFAULT NULL,
     * `ActiveOrderId` char(36) DEFAULT NULL,
     * `DirectorStarCount` int(11) NOT NULL,
     * PRIMARY KEY (`Id`)
     * ) ENGINE=InnoDB DEFAULT CHARSET=utf8
     */
    [Table(Name = "Agent")]
    public class Agent 
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UserId { get; set; }
        
        public string PathId { get; set; }
        
        public string Name { get; set; }
        

        public string Phone { get; set; }
        
        public string IdCardNo { get; set; }
        
        public string CrmUserId { get; set; }
        
        public AgentLevel AgentLevel { get; set; }
        
        public DateTime? ActivedOn { get; set; }
        
        public Guid? ActiveOrderId { get; set; }
        
        public int DirectorStarCount { get; set; }
    }
}
