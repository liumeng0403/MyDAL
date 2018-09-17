using EasyDAL.Test.Entities.EasyDal_Exchange;
using EasyDAL.Test.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Test.Query
{
    public class _04_QueryAllTest : TestBase
    {

        [Fact]
        public async Task Test01()
        {

            /********************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllAsync();
            Assert.True(res1.Count == 28620);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllAsync<AgentVM>();
            Assert.True(res2.Count == 28620);
            Assert.NotNull(res2.First().Name);
            Assert.Null(res2.First().XXXX);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************/

            var xx = "";

        }

    }
}
