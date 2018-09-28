using MyDAL.Test.Enums;
using System;

namespace MyDAL.Test.Options
{
    public class AgentQueryOption : PagingQueryOption
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
        public DateTime StartTime { get; set; }

        [QueryColumn("CreatedOn", CompareEnum.LessThanOrEqual)]
        public DateTime EndTime { get; set; }

        public AgentLevel AgentLevel { get; set; }
    }
}
