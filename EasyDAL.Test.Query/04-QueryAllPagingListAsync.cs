using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryM
{
    public class _04_QueryAllPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .QueryAllPagingListAsync(1, 10);
            Assert.True(res3.TotalCount == 28620);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

        }
    }
}
