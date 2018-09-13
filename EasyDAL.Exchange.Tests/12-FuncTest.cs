using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class FuncTest: TestBase
    {

        [Fact]
        public async Task CharLengthTest()
        {

            var xx1 = "";

            // .Where(a => a.Name.Length > 0)
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Name.Length > 2)
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xxR1 = "";

            // .Where(a => a.Name.Length > 0)
            var resR1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => 2 < it.Name.Length)
                .QueryListAsync();

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);
            Assert.True(res1.Count == resR1.Count);


            var xx2 = "";

            // .Where(a => a.Name.Length > 0)
            var res2 = await Conn.OpenDebug()
                .Joiner<Agent,AgentInventoryRecord>(out var agent,out var record)
                .From(()=>agent)
                .InnerJoin(()=>record)
                .On(()=>agent.Id==record.AgentId)
                .Where(() => agent.Name.Length > 2)
                .QueryListAsync<Agent>();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }


        // 查询 单值
        [Fact]
        public async Task CountTest()
        {
            var xx1 = "";

            // count(id)  like "陈%"
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .Count(it => it.Id)
                .QuerySingleValueAsync<long>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }


    }
}
