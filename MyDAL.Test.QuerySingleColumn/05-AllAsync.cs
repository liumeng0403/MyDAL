using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _05_AllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .AllAsync(it => it.Id);
            Assert.True(res1.Count == 28620);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
