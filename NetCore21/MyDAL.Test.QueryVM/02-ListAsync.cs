using MyDAL.Test.Entities;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _02_ListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;
            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************/

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .OrderBy(it => it.CreatedOn)
                .ListAsync<AgentVM>();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************/

            xx = string.Empty;

            var testQ5 = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                StartTime = WhereTest.CreatedOn,
                EndTime = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= testQ5.StartTime)
                .ListAsync<AgentVM>();
            Assert.True(res5.Count == 28619);
            Assert.NotNull(res5.First().Name);
            Assert.Null(res5.First().XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
