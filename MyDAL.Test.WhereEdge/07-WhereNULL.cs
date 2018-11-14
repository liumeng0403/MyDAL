using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _07_WhereNULL : TestBase
    {

        public async Task<Agent> PreData3()
        {
            var m = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("0001c614-dbef-4335-94b4-01654433a215"))
                .QueryFirstOrDefaultAsync();

            await Conn
                .Updater<Agent>()
                .Set(it => it.AgentLevel, WhereTest.AgentLevelNull)
                .Where(it => it.Id == m.Id)
                .UpdateAsync();

            return m;
        }
        public async Task ClearData3(Agent m)
        {
            await Conn
                .Updater<Agent>()
                .Set(it => it.AgentLevel, m.AgentLevel)
                .Where(it => it.Id == m.Id)
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

            var m = await PreData3();
            // 
            try
            {
                var res3 = await Conn
                    .Selecter<Agent>()
                    .Where(it => it.AgentLevel == WhereTest.AgentLevelNull)
                    .QueryListAsync();
            }
            catch (Exception ex)
            {
                var tuple3 = (XDebug.SQL, XDebug.Parameters);
                Assert.True(ex.Message.Equals("[[Convert(value(MyDAL.Test.WhereEdge._07_WhereNULL).WhereTest.AgentLevelNull, Nullable`1)]] 中,传入的 SQL 筛选条件为 Null !!!", StringComparison.OrdinalIgnoreCase));
            }

            await ClearData3(m);

            /************************************************************************************************************************/

            var xx4 = "";

            // is not null 
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActivedOn != null && it.ActiveOrderId != null && it.CrmUserId == null)
                .QueryListAsync();
            Assert.True(res4.Count == 554);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
