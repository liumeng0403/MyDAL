using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _04_NotEqual
        :TestBase
    {

        [Fact]
        public async Task NotEqual()
        {
            xx = string.Empty;

            // != --> <>
            var res1 = await Conn.QueryListAsync<Agent>(it => it.AgentLevel != AgentLevel.Customer);

            Assert.True(res1.Count == 556);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task NotNotEqual()
        {
            xx = string.Empty;

            // !(!=) --> =
            var res1 = await Conn.QueryListAsync<Agent>(it => !(it.AgentLevel != AgentLevel.Customer));

            Assert.True(res1.Count == 28064);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***********************************************************************************************************************/

            xx = string.Empty;

            // == --> =
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .OrderBy(it => it.CreatedOn)
                .QueryListAsync<AgentVM>();

            Assert.True(res2.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);
            
            /***********************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
