using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQuerySingleColumn
{
    public class _01_FirstOrDefaultAsync : TestBase
    {
        [Fact]
        public async Task Test()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord agentRecord)
                .From(() => agent)
                    .InnerJoin(() => agentRecord)
                        .On(() => agent.Id == agentRecord.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .FirstOrDefaultAsync(() => agent.Name);
            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
