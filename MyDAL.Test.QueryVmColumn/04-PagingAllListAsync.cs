using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _04_AllPagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .PagingAllListAsync(1, 10, it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });
            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var xx = "";
        }
    }
}
