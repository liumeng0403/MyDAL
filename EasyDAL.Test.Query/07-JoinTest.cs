using EasyDAL.Test.Entities.EasyDal_Exchange;
using EasyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Test.Query
{
    public class _07_JoinTest : TestBase
    {

        [Fact]
        public async Task TwoJoinTest()
        {

            var xx1 = "";

            var res1 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent1, out var record1)
                .From(() => agent1)
                .InnerJoin(() => record1)
                .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .And(() => record1.CreatedOn >= DateTime.Now.AddDays(-60))
                .QueryListAsync<Agent>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            var res2 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => record2.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync(() => new AgentVM
                {
                    nn = agent2.PathId,
                    yy = record2.Id,
                    xx = agent2.Id,
                    zz = agent2.Name,
                    mm = record2.LockedCount
                });

            var tuple2 = (XDebug.SQL, XDebug.Parameters);
            var yy2 = res2.First().nn;

            var xx = "";
        }

    }
}
