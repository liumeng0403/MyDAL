using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _03_PagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .PagingListAsync<AgentVM>(1, 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR6 = await Conn
                .Queryer<Agent>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .PagingListAsync<AgentVM>(1, 10);
            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);


            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
