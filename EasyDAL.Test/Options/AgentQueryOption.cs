using MyDAL.Test.Enums;
using System;
using Yunyong.Core;

namespace MyDAL.Test.Options
{
    public class AgentQueryOption : PagingQueryOption
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public AgentLevel AgentLevel { get; set; }
    }
}
