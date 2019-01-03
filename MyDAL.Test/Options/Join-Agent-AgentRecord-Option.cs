using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.Test.Options
{
    public class Join_Agent_AgentRecord_Option
        : PagingQueryOption
    {
        [XQuery(Table = typeof(Agent), Column = nameof(Agent.AgentLevel), Compare = CompareEnum.In)]
        public List<AgentLevel> Level { get; set; }

        [XQuery(Table = typeof(Agent), Column = nameof(Agent.Name), Compare = CompareEnum.Like_StartsWith)]
        public string Name { get; set; }

        [XQuery(Table = typeof(AgentInventoryRecord), Column = nameof(AgentInventoryRecord.CreatedOn), Compare = CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        [XQuery(Table = typeof(AgentInventoryRecord), Column = nameof(AgentInventoryRecord.CreatedOn), Compare = CompareEnum.LessThanOrEqual)]
        public DateTime EndTime { get; set; }
    }
}
