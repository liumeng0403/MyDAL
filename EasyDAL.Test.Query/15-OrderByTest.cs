using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Query
{
    public class _15_OrderByTest : TestBase
    {

        [Fact]
        public async Task OrderByXTest()
        {

            /******************************************************************************************************/

            var xx1 = "";

            // order by
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)128)
                .OrderBy(it => it.PathId)
                .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .QueryPagingListAsync(1, 10);
            Assert.True(res1.TotalCount == 555);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx2 = "";

            // key
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .QueryPagingListAsync(1, 10);
            Assert.True(res2.TotalCount == 28064);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx3 = "";

            // none key
            var res3 = await Conn
                .Selecter<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .QueryPagingListAsync(1, 10);
            Assert.True(res3.TotalPage == 56);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx = "";
        }

    }
}
