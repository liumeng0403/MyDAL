using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _02_QueryListAsync_History
        : TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************/

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryListAsync();

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx=string.Empty;

        }
    }
}
