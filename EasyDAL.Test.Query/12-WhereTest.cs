using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Query
{
    public class _12_WhereTest:TestBase
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
            catch(Exception ex)
            {
                var tuple3 = (XDebug.SQL, XDebug.Parameters);
                Assert.Equal(ex.Message,"条件筛选表达式【it => (Convert(it.AgentLevel, Nullable`1) == Convert(value(MyDAL.Test.Query._12_WhereTest).WhereTest.AgentLevelNull, Nullable`1))】中,条件值【AgentLevelNull】不能为 Null !");
            }

            await ClearData3(m);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
