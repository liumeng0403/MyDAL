using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;

namespace MyDAL.Test.Options
{
    public class AgentQueryOption 
        : PagingOption
    {
        public Guid? Id { get; set; }

        [XQuery(Compare = CompareEnum.Like)]
        public string Name { get; set; }

        [XQuery(Column = "CreatedOn", Compare = CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        [XQuery(Column = "CreatedOn", Compare = CompareEnum.LessThanOrEqual)]
        public DateTime EndTime { get; set; }

        public AgentLevel AgentLevel { get; set; }

        [XQuery(Column = "AgentLevel", Compare = CompareEnum.In)]
        public List<AgentLevel> EnumListIn { get; set; }

        [XQuery(Column = "AgentLevel", Compare = CompareEnum.NotIn)]
        public List<AgentLevel> EnumListNotIn { get; set; }
    }
}
