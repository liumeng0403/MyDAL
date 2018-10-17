using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;
using static MyDAL.Test.Query._08_WhereObjTest;

namespace MyDAL.Test.Query
{
    public class _04_QueryPagingListTest : TestBase
    {

        // 分页查询 m
        [Fact]
        public async Task QueryPagingListAsyncTest()
        {

            /*************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync(1, 10);
            Assert.True(res1.TotalCount == resR1.TotalCount);
            Assert.True(res1.TotalCount == 28619);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .QueryAllPagingListAsync(1, 10);
            Assert.True(res3.TotalCount == 28620);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent5, out var record5)
                .From(() => agent5)
                .InnerJoin(() => record5)
                .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync<Agent>(1, 10);
            Assert.True(res5.TotalCount == 574);

            /*************************************************************************************************************************/

            var xx = "";
        }

    }
}
