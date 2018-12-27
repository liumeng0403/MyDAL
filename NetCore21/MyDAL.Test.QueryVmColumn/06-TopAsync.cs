using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _06_TopAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res3.Count == 25);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx6 = "";

            //var res6 = await Conn
            //    .Queryer<Agent>()
            //    .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
            //    .ListAsync<AgentVM>(25, agent => new AgentVM
            //    {
            //        XXXX = agent.Name,
            //        YYYY = agent.PathId
            //    });
            //Assert.True(res6.Count == 25);

            //var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }
    }
}
