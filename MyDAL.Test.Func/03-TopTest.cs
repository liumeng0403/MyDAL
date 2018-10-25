using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Func
{
    public class _03_TopTest:TestBase
    {

        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .TopAsync(25);
            Assert.True(res1.Count == 25);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it=>it.AgentLevel== AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25);
            Assert.True(res2.Count == 25);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25,agent=>new AgentVM
                {
                    XXXX= agent.Name,
                    YYYY=agent.PathId
                });
            Assert.True(res3.Count == 25);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it=>it.AgentLevel== AgentLevel.Customer)
                .OrderBy(it=>it.PathId)
                .QueryListAsync(25);
            Assert.True(res4.Count == 25);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync<AgentVM>(25);
            Assert.True(res5.Count == 25);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx6 = "";

            var res6 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync<AgentVM>(25, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res6.Count == 25);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx7 = "";

            var res7 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent7, out var record7)
                .From(() => agent7)
                    .InnerJoin(() => record7)
                        .On(() => agent7.Id == record7.AgentId)
                .Where(() => record7.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync(25,() => new AgentVM
                {
                    nn = agent7.PathId,
                    yy = record7.Id,
                    xx = agent7.Id,
                    zz = agent7.Name,
                    mm = record7.LockedCount
                });
            Assert.True(res7.Count == 25);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************************/

            var xx8 = "";

            var res8 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent8, out var record8)
                .From(() => agent8)
                    .InnerJoin(() => record8)
                        .On(() => agent8.Id == record8.AgentId)
                .Where(() => record8.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync<Agent>(25);
            Assert.True(res8.Count == 25);

            var tuple8 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************************/

            var xx9 = "";

            var res9 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => record9.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync(25, () => new AgentVM
                {
                    nn = agent9.PathId,
                    yy = record9.Id,
                    xx = agent9.Id,
                    zz = agent9.Name,
                    mm = record9.LockedCount
                });
            Assert.True(res9.Count == 25);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /*******************************************************************************************************************************/

            var xx10 = "";

            var res10 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent10, out var record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => record10.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync<Agent>(25);
            Assert.True(res10.Count == 25);

            var tuple10 = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx = "";

        }

    }
}
