using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _04_PagingAllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .PagingAllAsync(1, 10, it => it.Id);
            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
