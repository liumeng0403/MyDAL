using MyDAL.Test.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
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
