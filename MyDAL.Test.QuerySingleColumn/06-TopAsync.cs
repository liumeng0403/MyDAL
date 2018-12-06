using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.Customer)
                .OrderBy(it => it.PathId)
                .ListAsync(25);
            Assert.True(res4.Count == 25);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx11 = "";

            var res11 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, it => it.Name);
            Assert.True(res11.Count == 25);

            var tuple11 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }
    }
}
