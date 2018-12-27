using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _04_PagingAllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var res1 = await Conn
                .Queryer<Agent>()
                .PagingAllAsync(1, 10, it => new AgentVM
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
