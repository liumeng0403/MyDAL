using MyDAL.Test.Entities;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _02_QueryListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;
            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************/

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
                .QueryListAsync<AgentVM>();
            Assert.True(res5.Count == 28619);
            Assert.NotNull(res5.First().Name);
            Assert.Null(res5.First().XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
