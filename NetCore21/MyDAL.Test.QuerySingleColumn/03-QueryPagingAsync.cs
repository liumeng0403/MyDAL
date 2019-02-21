using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _03_QueryPagingAsync 
        : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;
            
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.StartsWith("张"))
                .OrderBy(it=>it.Name, OrderByEnum.Desc)
                .QueryPagingAsync(1,10, it => it.Name);

            Assert.True(res2.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
