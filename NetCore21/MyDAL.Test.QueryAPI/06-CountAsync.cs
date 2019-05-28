using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _06_CountAsync
        : TestBase
    {
        [Fact]
        public async Task Count_Star_Shortcut()
        {
            xx = string.Empty;

            var res1 = await Conn.CountAsync<Agent>(it => it.Name.Length > 3);

            Assert.True(res1 == 116);

            xx = string.Empty;
        }

        [Fact]
        public async Task Count_SpecialColumn_Shortcut()
        {

        }

        [Fact]
        public async Task Count_Star_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains("陈%"))
                .CountAsync();

            Assert.True(res1 == 1421);

            xx = string.Empty;
        }

        [Fact]
        public async Task Count_SpecialColumn_ST()
        {
            xx = string.Empty;

            // count(id)  like "陈%"
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains("陈%"))
                .CountAsync(it => it.Id);

            Assert.True(res1 == 1421);

            xx = string.Empty;
        }

        [Fact]
        public async Task Count_Star_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Contains("陈%"))
                .CountAsync();

            Assert.True(res1 == 24);

            xx = string.Empty;
        }

        [Fact]
        public async Task Count_SpecialColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent4, out AgentInventoryRecord record4)
                .From(() => agent4)
                    .InnerJoin(() => record4)
                        .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name.Contains("陈%"))
                .CountAsync(() => agent4.Id);

            Assert.True(res1 == 24);

            xx = string.Empty;
        }

        [Fact]
        public async Task SqlAction_Distinct_Count_SpecialColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .CountAsync(it => it.AgentLevel);

            Assert.Equal(3, res1);

            xx = string.Empty;
        }
    }
}
