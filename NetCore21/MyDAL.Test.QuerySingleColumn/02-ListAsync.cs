using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _02_ListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(it => it.Name);
            Assert.True(res2.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
