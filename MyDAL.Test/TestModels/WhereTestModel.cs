using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;

namespace MyDAL.Test.Entities
{
    public class WhereTestModel
    {
        public int Start { get; set; }
        public int End { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DateTime_大于等于 { get; set; }
        public DateTime DateTime_小于等于 { get; set; }

        public AgentLevel AgentLevelXX { get; set; }
        public AgentLevel? AgentLevelNull { get; set; }

        public string ContainStr { get; set; }

        public List<AgentLevel?> In_List_枚举 { get; set; }

        public AgentLevel?[] In_Array_枚举 { get; set; }

        public List<string> In_List_String { get; set; }
        public string[] In_Array_String { get; set; }

        public string Name { get; set; }
    }
}
