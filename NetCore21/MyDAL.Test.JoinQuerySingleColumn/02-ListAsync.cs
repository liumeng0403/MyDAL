using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.JoinQuerySingleColumn
{
    public class _02_ListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;
            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.AgentLevel == AgentLevel.DistiAgent)
                .ListAsync(() => agent1.CreatedOn);
            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
