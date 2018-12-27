using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _06_ExistTest : TestBase
    {

        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            /*****************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();
            Assert.True(res1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*****************************************************************************************/

            xx=string.Empty;
        }

    }
}
