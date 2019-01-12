using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _03_PagingListAsync_Option
        : TestBase
    {
        [Fact]
        public async Task Test()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option = new AgentQueryOption
            {
                Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"),
                Name = "樊士芹"
            };
            // where method -- option orderby 
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option)
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .PagingListAsync<AgentVM>();
            Assert.True(res3.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option13 = new AgentQueryOption();
            var res13 = await Conn
                .Queryer<Agent>()
                .Where(option13)
                .PagingListAsync<AgentVM>();
            Assert.True(res13.TotalCount == 28620);
            Assert.True(res13.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
