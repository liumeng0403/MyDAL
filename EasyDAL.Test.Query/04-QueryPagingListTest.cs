using EasyDAL.Test.Entities.EasyDal_Exchange;
using EasyDAL.Test.Enums;
using EasyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Test.Query
{
    public class _04_QueryPagingListTest : TestBase
    {

        // 分页查询 m
        [Fact]
        public async Task QueryPagingListAsyncTest()
        {

            /*************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync(1, 10);
            Assert.True(res1.TotalCount == resR1.TotalCount);
            Assert.True(res1.TotalCount == 28619);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var resR2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);
            Assert.True(res2.TotalCount == resR2.TotalCount);
            Assert.True(res2.TotalCount == 28619);

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllPagingListAsync(1, 10);
            Assert.True(res3.TotalCount == 28620);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllPagingListAsync<AgentVM>(1, 10);
            Assert.True(res4.TotalCount == 28620);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.OpenDebug()
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
