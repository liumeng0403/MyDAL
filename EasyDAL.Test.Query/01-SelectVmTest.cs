using MyDAL.Test.Entities;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Query
{
    public class _01_SelectVmTest:TestBase
    {

        [Fact]
        public async Task test()
        {

            /****************************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync<AgentVM>();
            Assert.NotNull(res1);
            Assert.Null(res1.XXXX);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx2 = "";

            var guid2 = Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef");
            var res2 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Id == guid2)
                .QueryFirstOrDefaultAsync(() => new AgentVM
                {
                    nn = agent2.PathId,
                    yy = record2.Id,
                    xx = agent2.Id,
                    zz = agent2.Name,
                    mm = record2.LockedCount
                });
            Assert.NotNull(res2);
            Assert.Equal("夏明君", res2.zz);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.Equal("樊士芹", res3.XXXX);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx4 = "";

            var testQ4 = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                DateTime_大于等于 = WhereTest.CreatedOn,
                DateTime_小于等于 = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testQ4.DateTime_大于等于)
                .QueryListAsync<AgentVM>();
            Assert.True(res4.Count == 28619);
            Assert.NotNull(res4.First().Name);
            Assert.Null(res4.First().XXXX);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res5.Count == 555);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx6 = "";

            var res6 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            var resR6 = await Conn
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);
            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            var tupleR6 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx7 = "";

            var res7 = await Conn
                .Selecter<Agent>()
                .QueryAllPagingListAsync<AgentVM>(1, 10);
            Assert.True(res7.TotalCount == 28620);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx8 = "";

            var res8 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            var tuple8 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx9 = "";

            var res9 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                .InnerJoin(() => record9)
                .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent9.Name,
                    YYYY = agent9.PathId
                });
            Assert.True(res9.TotalCount == 574);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx10 = "";

            var option10 = new AgentQueryOption();
            option10.AgentLevel = AgentLevel.DistiAgent;
            var res10 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent10, out var record10)
                .From(() => agent10)
                .InnerJoin(() => record10)
                .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(option10, () => new AgentVM
                {
                    XXXX = agent10.Name,
                    YYYY = agent10.PathId
                });
            Assert.True(res10.TotalCount == 574);

            var tuple10 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx11 = "";

            var option11 = new AgentQueryOption();
            option11.AgentLevel = AgentLevel.DistiAgent;
            option11.PageIndex = 5;
            option11.PageSize = 10;
            var res11 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent11, out var record11)
                .From(() => agent11)
                .InnerJoin(() => record11)
                .On(() => agent11.Id == record11.AgentId)
                .Where(() => agent11.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(option11, () => new AgentVM
                {
                    XXXX = agent11.Name,
                    YYYY = agent11.PathId
                });
            Assert.True(res11.TotalCount == 574);
            Assert.True(res11.PageSize == 10);
            Assert.True(res11.PageIndex == 5);
            Assert.True(res11.Data.Count == 10);

            var tuple11 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx12 = "";

            var res12 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent12, out var record12)
                .From(() => agent12)
                .InnerJoin(() => record12)
                .On(() => agent12.Id == record12.AgentId)
                .Where(() => record12.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync(() => new AgentVM
                {
                    nn = agent12.PathId,
                    yy = record12.Id,
                    xx = agent12.Id,
                    zz = agent12.Name,
                    mm = record12.LockedCount
                });
            Assert.True(res12.Count == 574);

            var tuple12 = (XDebug.SQL, XDebug.Parameters);
            var yy2 = res12.First().nn;

        }

    }
}
