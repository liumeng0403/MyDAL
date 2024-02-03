using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using Xunit;

namespace MyDAL.QueryAPI
{
    public class _07_SumAsync
        : TestBase
    {
        [Fact]
        public void Sum_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<AlipayPaymentRecord>()
                .Where(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .Sum(it => it.TotalAmount);

            Assert.True(res1 == 1527.2600000000000000000000000M);

            xx = string.Empty;
        }

        [Fact]
        public void Sum_Nullable_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB.OpenDebug()
                .Selecter<AgentInventoryRecord>()
                .Where(it => it.Id != Guid.Parse("df2b788e-6b1a-4a74-ac1d-016551f76dc9"))
                .Sum(it => it.TotalSaleCount);

            Assert.True(res1 == 589);

            xx = string.Empty;
        }

        [Fact]
        public void Sum_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter(out Agent a, out AgentInventoryRecord air)
                .From(() => a)
                    .InnerJoin(() => air)
                        .On(() => a.Id == air.AgentId)
                .Where(() => a.AgentLevel == AgentLevel.DistiAgent)
                .Sum(() => air.LockedCount);

            Assert.Equal(0, res1);

            xx = string.Empty;
        }

        [Fact]
        public void Sum_Nullable_MT()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB.OpenDebug()
                .Selecter(out Agent a, out AgentInventoryRecord air)
                .From(() => a)
                    .InnerJoin(() => air)
                        .On(() => a.Id == air.AgentId)
                .Where(() => a.AgentLevel == AgentLevel.DistiAgent)
                .Sum(() => air.TotalSaleCount);

            Assert.Equal(589, res1.Value);

            xx = string.Empty;
        }
    }
}
