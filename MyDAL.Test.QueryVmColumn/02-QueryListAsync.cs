using MyDAL.Test.Entities;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _02_QueryListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

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

        }
    }
}
