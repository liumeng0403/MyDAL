using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryM
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            xx = "";

            var res8 = await Conn
                .Queryer(out Agent agent8, out AgentInventoryRecord record8)
                .From(() => agent8)
                    .InnerJoin(() => record8)
                        .On(() => agent8.Id == record8.AgentId)
                .Where(() => record8.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync<Agent>(25);
            Assert.True(res8.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = "";

            var res10 = await Conn
                .Queryer(out Agent agent10, out AgentInventoryRecord record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => record10.CreatedOn >= WhereTest.CreatedOn)
                .ListAsync<Agent>(25);
            Assert.True(res10.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }
    }
}
