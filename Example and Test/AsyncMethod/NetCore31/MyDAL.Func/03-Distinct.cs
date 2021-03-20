using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _03_Distinct : TestBase
    {
        [Fact]
        public async Task test()
        {
            /*************************************************************************************************************************/

            xx = string.Empty;

            var res2 = await MyDAL_TestDB
                .Queryer<Agent>()
                .Distinct()
                .QueryListAsync(it => it.Name);

            Assert.True(res2.Count == 24444);
             
            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res6 = await MyDAL_TestDB
                .Queryer<Agent>()
                .Where(it => it.Name == "刘中华")
                .Distinct()
                .QueryOneAsync();

            Assert.NotNull(res6);
            var res61 = await MyDAL_TestDB.QueryListAsync<Agent>(it => it.Name == "刘中华");
            Assert.True(res61.Count == 2);

            

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res7 = await MyDAL_TestDB
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.AgentLevel == AgentLevel.DistiAgent)
                .Distinct()
                .QueryListAsync(() => agent1.Name);

            Assert.True(res7.Count == 543);

            

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
