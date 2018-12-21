using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _05_AllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .AllAsync<AgentVM>();
            Assert.True(res2.Count == 28620);
            Assert.NotNull(res2.First().Name);
            Assert.Null(res2.First().XXXX);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx = string.Empty;

        }
    }
}
