using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.JoinQueryM
{
    public class _02_QueryListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx1 = "";

            var res1 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent1, out var record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                    .And(() => record1.CreatedOn >= WhereTest.CreatedOn.AddDays(-60))
                .QueryListAsync<Agent>();
            Assert.True(res1.Count == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }
    }
}
