using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities
{
    public class WhereTestModel
    {
        public int Start { get; set; }
        public int End { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DateTime_大于等于 { get; set; }
        public DateTime DateTime_小于等于 { get; set; }

        public AgentLevel AgentLevelXX { get; set; }

        public string ContainStr { get; set; }
    }
}
