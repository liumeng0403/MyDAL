using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _04_PagingAllListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            xx=string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .PagingAllAsync(1, 10);
            Assert.True(res3.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

        }
    }
}
