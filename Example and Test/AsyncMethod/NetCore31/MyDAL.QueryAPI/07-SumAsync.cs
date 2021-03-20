using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.QueryAPI
{
    public class _07_SumAsync
        : TestBase
    {
        [Fact]
        public async Task Sum_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer<AlipayPaymentRecord>()
                .Where(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SumAsync(it => it.TotalAmount);

            Assert.True(res1 == 1527.2600000000000000000000000M);

            xx = string.Empty;
        }

        [Fact]
        public async Task Sum_Nullable_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer<AgentInventoryRecord>()
                .Where(it => it.Id != Guid.Parse("df2b788e-6b1a-4a74-ac1d-016551f76dc9"))
                .SumAsync(it => it.TotalSaleCount);

            Assert.True(res1 == 589);

            xx = string.Empty;
        }

        [Fact]
        public async Task Sum_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer(out Agent a, out AgentInventoryRecord air)
                .From(() => a)
                    .InnerJoin(() => air)
                        .On(() => a.Id == air.AgentId)
                .Where(() => a.AgentLevel == AgentLevel.DistiAgent)
                .SumAsync(() => air.LockedCount);

            Assert.Equal(0, res1);

            xx = string.Empty;
        }

        [Fact]
        public async Task Sum_Nullable_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer(out Agent a, out AgentInventoryRecord air)
                .From(() => a)
                    .InnerJoin(() => air)
                        .On(() => a.Id == air.AgentId)
                .Where(() => a.AgentLevel == AgentLevel.DistiAgent)
                .SumAsync(() => air.TotalSaleCount);

            Assert.Equal(589, res1.Value);

            xx = string.Empty;
        }
    }
}
