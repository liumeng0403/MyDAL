using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _03_PagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync(1, 10, it => it.Name);
            Assert.True(res1.Data.Count == 10);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option2 = new AgentQueryOption();
            option2.OrderBys = new List<OrderBy>
            {
                new OrderBy
                {
                    Field="Name",
                    Desc=true
                }
            };

            // where method -- option orderby 
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.StartsWith("张"))
                .PagingListAsync(option2, it => it.Name);
            Assert.True(res2.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
