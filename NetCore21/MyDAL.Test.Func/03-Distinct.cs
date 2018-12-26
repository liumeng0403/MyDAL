using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _03_Distinct : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;
            var tuple = default((List<string>, List<string>, List<string>));

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.Name);
            Assert.True(res2.Count == 24444);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy-MM-dd"));
            Assert.True(res3.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy-MM"));
            Assert.True(res4.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy"));
            Assert.True(res5.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name == "刘中华")
                .Distinct()
                .FirstOrDefaultAsync();
            Assert.NotNull(res6);
            var res61 = await Conn.ListAsync<Agent>(it => it.Name == "刘中华");
            Assert.True(res61.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res7 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.AgentLevel == AgentLevel.DistiAgent)
                .Distinct()
                .ListAsync(() => agent1.Name);

            Assert.True(res7.Count == 543);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
