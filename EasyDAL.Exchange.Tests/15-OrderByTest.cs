using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using EasyDAL.Exchange.Tests.Enums;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Exchange.Tests
{
    public class OrderByTest : TestBase
    {

        [Fact]
        public async Task OrderByXTest()
        {
            var xx1 = "";

            // order by


            var xx2 = "";

            // key
            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .QueryPagingListAsync(1, 10);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            // none key
            var res3 = await Conn.OpenDebug()
                .Selecter<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .QueryPagingListAsync(1,10);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
