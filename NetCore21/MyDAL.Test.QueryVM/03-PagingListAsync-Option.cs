using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _03_PagingListAsync_Option
        :TestBase
    {
        [Fact]
        public async Task Test()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option = new AgentQueryOption();
            option.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option.Name = "樊士芹";
            option.OrderBys = new List<OrderBy>
            {
                new OrderBy
                {
                    Field="Name",
                    Desc=true
                }
            };
            // where method -- option orderby 
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option)
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

        }
    }
}
