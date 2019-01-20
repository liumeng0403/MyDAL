using MyDAL.Test.Entities.MyDAL_TestDB;
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

            xx = string.Empty;

            // is null 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .QueryListAsync();
            Assert.True(res1.Count == 28066);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            // is not null 
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .QueryListAsync();
            Assert.True(res2.Count == 554);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx3 = string.Empty;

            var m = await PreData3();
            // 
            try
            {
                var res3 = await Conn
                    .Queryer<Agent>()
                    .Where(it => it.AgentLevel == WhereTest.AgentLevelNull)
                    .QueryListAsync();
            }
            catch (Exception ex)
            {
                tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);
                Assert.Equal("[[Convert(value(MyDAL.Test.WhereEdge._07_WhereNULL).WhereTest.AgentLevelNull, Nullable`1)]] 中,传入的 SQL 筛选条件为 Null !!!", ex.Message, ignoreCase: true);
            }

            await ClearData3(m);

            /************************************************************************************************************************/

            var xx4 = string.Empty;

            // is not null 
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActivedOn != null && it.ActiveOrderId != null && it.CrmUserId == null)
                .QueryListAsync();
            Assert.True(res4.Count == 554);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx5 = string.Empty;

            // is not null 
            var res5 = await Conn
                .Queryer(out Agent a5,out AgentInventoryRecord r5)
                .From(()=>a5)
                    .LeftJoin(()=>r5)
                        .On(()=>a5.Id==r5.AgentId)
                .Where(() => a5.ActiveOrderId==null)
                .QueryListAsync<Agent>();
            Assert.True(res5.Count == 28085);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            var xx6 = string.Empty;

            // is not null 
            var res6 = await Conn
                .Queryer(out Agent a6, out AgentInventoryRecord r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => a6.ActiveOrderId != null)
                .QueryListAsync<Agent>();
            Assert.True(res6.Count == 554);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            var res7 = await Conn.QueryListAsync<Agent>(it => it.ActiveOrderId == null);
            Assert.True(res7.Count == 28066);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            // is not null 
            var res8 = await Conn.QueryListAsync<Agent>(it => it.ActiveOrderId != null);
            Assert.True(res8.Count == 554);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
