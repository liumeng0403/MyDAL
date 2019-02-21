using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _03_QueryPagingAsync_History
        : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            xx=string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync(1, 10);

            Assert.True(res3.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

        }
    }
}
