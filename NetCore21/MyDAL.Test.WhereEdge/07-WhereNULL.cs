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
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("0001c614-dbef-4335-94b4-01654433a215"))
                .FirstOrDefaultAsync();

            await Conn
                .Updater<Agent>()
                .Set(it => it.AgentLevel, WhereTest.AgentLevelNull)
                .Where(it => it.Id == m.Id)
                .UpdateAsync();

            return m;
        }
        private async Task ClearData3(Agent m)
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
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .ListAsync();
            Assert.True(res1.Count == 28066);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx2 = "";

            // is not null 
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .ListAsync();
            Assert.True(res2.Count == 554);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx3 = "";

            var m = await PreData3();
            // 
            try
            {
                var res3 = await Conn
                    .Queryer<Agent>()
                    .Where(it => it.AgentLevel == WhereTest.AgentLevelNull)
                    .ListAsync();
            }
            catch (Exception ex)
            {
                var tuple3 = (XDebug.SQL, XDebug.Parameters);
                Assert.Equal("[[Convert(value(MyDAL.Test.WhereEdge._07_WhereNULL).WhereTest.AgentLevelNull, Nullable`1)]] 中,传入的 SQL 筛选条件为 Null !!!", ex.Message, ignoreCase: true);
            }

            await ClearData3(m);

            /************************************************************************************************************************/

            var xx4 = "";

            // is not null 
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActivedOn != null && it.ActiveOrderId != null && it.CrmUserId == null)
                .ListAsync();
            Assert.True(res4.Count == 554);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx5 = "";

            // is not null 
            var res5 = await Conn
                .Queryer<Agent,AgentInventoryRecord>(out var a5,out var r5)
                .From(()=>a5)
                    .LeftJoin(()=>r5)
                        .On(()=>a5.Id==r5.AgentId)
                .Where(() => a5.ActiveOrderId==null)
                .ListAsync<Agent>();
            Assert.True(res5.Count == 28085);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx6 = "";

            // is not null 
            var res6 = await Conn
                .Queryer<Agent, AgentInventoryRecord>(out var a6, out var r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => a6.ActiveOrderId != null)
                .ListAsync<Agent>();
            Assert.True(res6.Count == 554);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
