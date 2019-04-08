using HPC.DAL;
using MyDAL.Test.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
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
    [XTable(Name ="Agent")]
    public class Agent 
    {
        public Agent() { }
        public Agent(string s1, int i2, decimal d3) { }

        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UserId { get; set; }
        
        public string PathId { get; set; }
        
        public string Name { get; set; }
        

        public string Phone { get; set; }
        
        public string IdCardNo { get; set; }
        
        public string CrmUserId { get; set; }
        
        public AgentLevel? AgentLevel { get; set; }
        
        public DateTime? ActivedOn { get; set; }
        
        public Guid? ActiveOrderId { get; set; }
        
        public int DirectorStarCount { get; set; }
    }
}
