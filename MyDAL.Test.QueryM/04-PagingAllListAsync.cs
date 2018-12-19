using MyDAL.Test.Entities.EasyDal_Exchange;
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

            var xx3 = "";

            var res3 = await Conn
                .Queryer<Agent>()
                .PagingAllAsync(1, 10);
            Assert.True(res3.TotalCount == 28620);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

        }
    }
}
