using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQuerySingleColumn
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = string.Empty;

            var res1 = await Conn
                .Queryer<Agent, AgentInventoryRecord>(out var agent, out var record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, () => agent.Name);
            Assert.True(res1.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
