using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Func
{
    public class _06_CountTest : TestBase
    {

        [Fact]
        public async Task CountXTest()
        {

            /*
             *count
             */
            /************************************************************************************************************************/



            /************************************************************************************************************************/

            var xx2 = "";

            // count(id)  like "陈%"
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .CountAsync(it => it.Id);
            Assert.True(res2 == 1421);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx22 = "";

            var res22 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .CountAsync();
            Assert.True(res22 == 1421);

            var tuple22 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                .InnerJoin(() => record3).On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Contains(LikeTest.百分号))
                .CountAsync();
            Assert.True(res3 == 24);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent4, out var record4)
                .From(() => agent4)
                .InnerJoin(() => record4).On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name.Contains(LikeTest.百分号))
                .CountAsync(() => agent4.Id);
            Assert.True(res4 == 24);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
