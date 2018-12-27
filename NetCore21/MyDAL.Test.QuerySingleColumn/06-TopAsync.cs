using MyDAL.Test.Entities.MyDAL_TestDB;
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
            
            var xx = string.Empty;

            //var res4 = await Conn
            //    .Queryer<Agent>()
            //    .Where(it => it.AgentLevel == AgentLevel.Customer)
            //    .OrderBy(it => it.PathId)
            //    .ListAsync(25);
            //Assert.True(res4.Count == 25);

            //var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;

            var res11 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, it => it.Name);
            Assert.True(res11.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/


            xx = string.Empty;

        }
    }
}
