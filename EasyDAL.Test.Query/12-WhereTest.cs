using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Query
{
    public class _12_WhereTest:TestBase
    {

        public async void PreData3()
        {
            WhereTest.AgentLevelXX = null;

            await Conn
                .Updater<Agent>()
                .Set(it => it.AgentLevel, WhereTest.AgentLevelXX)
                .Where(it => it.Id == Guid.Parse("0001c614-dbef-4335-94b4-01654433a215"))
                .UpdateAsync();
        }

        [Fact]
        public async Task WhereTestx()
        {

            /************************************************************************************************************************/

            var xx1 = "";

            // is null 
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .QueryListAsync();
            Assert.True(res1.Count == 28066);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx2 = "";

            // is not null 
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .QueryListAsync();
            Assert.True(res2.Count == 554);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx3 = "";

            PreData3();
            WhereTest.AgentLevelXX = null;
            // 
            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == WhereTest.AgentLevelXX)
                .QueryListAsync();
            Assert.True(res2.Count == 1);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
