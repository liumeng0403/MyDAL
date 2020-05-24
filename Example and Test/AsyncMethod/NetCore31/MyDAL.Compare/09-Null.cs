using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _09_Null
        : TestBase
    {


        public async Task<Agent> PreData3()
        {
            var m = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("0001c614-dbef-4335-94b4-01654433a215"))
                .QueryOneAsync();

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
            xx = string.Empty;

            /************************************************************************************************************************/

            // is not null 
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .QueryListAsync();
            Assert.True(res2.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

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
                
                var errStr = "【ERR-078】 -- [[[[Convert(value(MyDAL.Test.Compare._09_Null).WhereTest.AgentLevelNull, Nullable`1)]] 中,传入的 SQL 筛选条件为 Null !!!]] ，请 EMail: --> liumeng0403@163.com <--";
                Assert.Equal(errStr, ex.Message, ignoreCase: true);
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

            

            /************************************************************************************************************************/

            var xx5 = string.Empty;

            // is not null 
            var res5 = await Conn
                .Queryer(out Agent a5, out AgentInventoryRecord r5)
                .From(() => a5)
                    .LeftJoin(() => r5)
                        .On(() => a5.Id == r5.AgentId)
                .Where(() => a5.ActiveOrderId == null)
                .QueryListAsync<Agent>();
            Assert.True(res5.Count == 28085);

            

            /************************************************************************************************************************/

            var res7 = await Conn.QueryListAsync<Agent>(it => it.ActiveOrderId == null);
            Assert.True(res7.Count == 28066);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // is not null 
            var res8 = await Conn.QueryListAsync<Agent>(it => it.ActiveOrderId != null);
            Assert.True(res8.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }


        [Fact]
        public async Task IsNull()
        {

            xx = string.Empty;

            // is null 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .QueryListAsync();

            Assert.True(res1.Count == 28066);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotIsNull()
        {

            xx = string.Empty;

            // !(is null) --> is not null 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => !(it.ActiveOrderId == null))
                .QueryListAsync();

            Assert.True(res1.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task IsNotNull()
        {

            xx = string.Empty;

            // is not null 
            var res6 = await Conn
                .Queryer(out Agent a6, out AgentInventoryRecord r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => a6.ActiveOrderId != null)
                .QueryListAsync<Agent>();

            Assert.True(res6.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotIsNotNull()
        {

            xx = string.Empty;

            // !(is not null) --> is null 
            var res6 = await Conn
                .Queryer(out Agent a6, out AgentInventoryRecord r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => !(a6.ActiveOrderId != null))
                .QueryListAsync<Agent>();

            Assert.True(res6.Count == 28085);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
