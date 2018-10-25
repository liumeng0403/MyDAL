using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _01_QueryFirstOrDefaultAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync<AgentVM>();
            Assert.NotNull(res1);
            Assert.Null(res1.XXXX);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx2 = "";

            var guid2 = Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef");
            var res2 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Id == guid2)
                .QueryFirstOrDefaultAsync(() => new AgentVM
                {
                    nn = agent2.PathId,
                    yy = record2.Id,
                    xx = agent2.Id,
                    zz = agent2.Name,
                    mm = record2.LockedCount
                });
            Assert.NotNull(res2);
            Assert.Equal("夏明君", res2.zz);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.Equal("樊士芹", res3.XXXX);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

        }
    }
}
