using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;

namespace MyDAL.Test.Options
{
    public class Join_AgentQueryOption
        :PagingOption
    {
        [XQuery(Table =typeof(Agent))]
        public AgentLevel AgentLevel { get; set; }
    }
}
