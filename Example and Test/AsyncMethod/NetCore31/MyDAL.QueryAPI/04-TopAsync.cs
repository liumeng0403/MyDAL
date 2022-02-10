using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.QueryAPI
{
    public class _04_TopAsync
        : TestBase
    {

        [Fact]
        public async Task SelectSingleColumn_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, it => it.Name);

            Assert.True(res1.Count == 25);

            

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectM_ST()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .TopAsync(25);

            Assert.True(res1.Count == 25);

            

        }

        [Fact]
        public async Task SelectVM_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync<AgentVM>(25);

            Assert.True(res1.Count == 25);

            

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectVmColumn_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res1.Count == 25);

            

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectSingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .TopAsync(25, () => agent.Name);

            Assert.True(res1.Count == 25);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectM_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent8, out AgentInventoryRecord record8)
                .From(() => agent8)
                    .InnerJoin(() => record8)
                        .On(() => agent8.Id == record8.AgentId)
                .Where(() => record8.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync<Agent>(25);

            Assert.True(res1.Count == 25);

            

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectVmColumn_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => record.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync(25, () => new AgentVM
                {
                    nn = agent.PathId,
                    yy = record.Id,
                    xx = agent.Id,
                    zz = agent.Name,
                    mm = record.LockedCount
                });

            Assert.True(res1.Count == 25);

            

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
