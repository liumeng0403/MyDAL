using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _06_CountTest : TestBase
    {

        [Fact]
        public async Task CountXTest()
        {

            /*
             *count
             */
            /************************************************************************************************************************/



            /************************************************************************************************************************/

            var xx = string.Empty;
            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            // count(id)  like "陈%"
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .CountAsync(it => it.Id);
            Assert.True(res2 == 1421);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            var res22 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .CountAsync();
            Assert.True(res22 == 1421);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Contains(LikeTest.百分号))
                .CountAsync();
            Assert.True(res3 == 24);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer(out Agent agent4, out AgentInventoryRecord record4)
                .From(() => agent4)
                    .InnerJoin(() => record4)
                        .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name.Contains(LikeTest.百分号))
                .CountAsync(() => agent4.Id);
            Assert.True(res4 == 24);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
