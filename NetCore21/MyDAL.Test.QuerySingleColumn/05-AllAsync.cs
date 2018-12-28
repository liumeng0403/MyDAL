using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _05_AllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .AllAsync(it => it.Id);
            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
