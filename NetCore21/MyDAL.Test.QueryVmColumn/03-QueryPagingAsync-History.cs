using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _03_QueryPagingAsync_History
        : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync(1, 10, it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
