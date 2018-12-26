using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _03_PagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .PagingListAsync<AgentVM>(1, 10);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR6 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .PagingListAsync<AgentVM>(1, 10);
            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);


            /*************************************************************************************************************************/

            xx = string.Empty;

            var option13 = new AgentQueryOption();
            var res13 = await Conn
                .Queryer<Agent>()
                .Where(option13.GetCondition())
                .PagingListAsync<AgentVM>(option13);
            Assert.True(res13.TotalCount == 28620);
            Assert.True(res13.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
