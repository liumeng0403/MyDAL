using MyDAL.Test.Entities;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _02_QueryListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            /*************************************************************************************************************************/

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res5.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
