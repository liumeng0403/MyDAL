using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQuerySingleColumn
{
    public class _01_FirstOrDefaultAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var res1 = await Conn
                .Queryer<Agent, AgentInventoryRecord>(out var agent, out var agentRecord)
                .From(() => agent)
                    .InnerJoin(() => agentRecord)
                        .On(() => agent.Id == agentRecord.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .FirstOrDefaultAsync<string>(() => agent.Name);
            Assert.NotNull(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var xx = "";
        }
    }
}
