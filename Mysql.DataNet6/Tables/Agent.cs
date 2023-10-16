using MyDAL;
using Mysql.Data_Net6.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysql.Data_Net6.Tables
{
    [XTable(Name = "Agent")]
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
