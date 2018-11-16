using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _05_AllAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx2 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .AllAsync<AgentVM>();
            Assert.True(res2.Count == 28620);
            Assert.NotNull(res2.First().Name);
            Assert.Null(res2.First().XXXX);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************/

        }
    }
}
