using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

            var xx = "";

        }

    }
}
