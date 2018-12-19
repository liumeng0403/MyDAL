using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25);
            Assert.True(res2.Count == 25);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .ListAsync<AgentVM>(25);
            Assert.True(res5.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
