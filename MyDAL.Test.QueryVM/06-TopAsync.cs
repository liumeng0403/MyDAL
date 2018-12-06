using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25);
            Assert.True(res2.Count == 25);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .ListAsync<AgentVM>(25);
            Assert.True(res5.Count == 25);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }
    }
}
