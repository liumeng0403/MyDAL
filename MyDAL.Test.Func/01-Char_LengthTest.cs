using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _01_Char_LengthTest : TestBase
    {

        [Fact]
        public async Task Char_LengthTest()
        {

            /*
             *char_length
             */
            /************************************************************************************************************************/

            var xx1 = "";

            // .Where(a => a.Name.Length > 0)
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Length > 2)
                .QueryListAsync();
            Assert.True(res1.Count == 22660);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xxR1 = "";

            // .Where(a => a.Name.Length > 0)
            var resR1 = await Conn
                .Selecter<Agent>()
                .Where(it => 2 < it.Name.Length)
                .QueryListAsync();
            Assert.True(res1.Count == resR1.Count);
            Assert.True(res1.Count == 22660);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx2 = "";

            // .Where(a => a.Name.Length > 0)
            var res2 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name.Length > 2)
                .QueryListAsync<Agent>();
            Assert.True(res2.Count == 457);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx3 = "";

            // .Where(a => a.Name.Length > 0)
            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Length > 2)
                .OrderBy(()=>agent3.Name.Length)
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 457);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx = "";
        }


    }
}
