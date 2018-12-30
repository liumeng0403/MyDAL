using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _05_QueryAllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .QueryAllAsync<AgentVM>();
            Assert.True(res2.Count == 28620);
            Assert.NotNull(res2.First().Name);
            Assert.Null(res2.First().XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx = string.Empty;

        }
    }
}
