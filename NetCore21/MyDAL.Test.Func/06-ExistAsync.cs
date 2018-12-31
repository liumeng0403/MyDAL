using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _06_ExistAsync : TestBase
    {

        // 查询 是否存在
        [Fact]
        public async Task Test()
        {
            /*****************************************************************************************/

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .ExistAsync();
            Assert.True(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************/

            xx = string.Empty;

            var pk2 = Guid.Parse("002c1ca9-f2df-453a-87e0-0165443dcc31");
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == pk2)
                .ExistAsync();
            Assert.True(res2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .ExistAsync();
            Assert.True(res3);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer(out Agent agent4, out AgentInventoryRecord record4)
                .From(() => agent4)
                    .InnerJoin(() => record4)
                        .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Id == pk2)
                .ExistAsync();
            Assert.True(res4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************/

            xx = string.Empty;
        }

    }
}
