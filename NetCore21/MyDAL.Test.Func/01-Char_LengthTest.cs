using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

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

            var xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Length > 2)
                .ListAsync();
            Assert.True(res1.Count == 22660);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var resR1 = await Conn
                .Queryer<Agent>()
                .Where(it => 2 < it.Name.Length)
                .ListAsync();
            Assert.True(res1.Count == resR1.Count);
            Assert.True(res1.Count == 22660);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res2 = await Conn
                .Queryer(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name.Length > 2)
                .ListAsync<Agent>();
            Assert.True(res2.Count == 457);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Length > 2)
                .OrderBy(() => agent3.Name.Length)
                .ListAsync<Agent>();
            Assert.True(res3.Count == 457);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;
        }


    }
}
