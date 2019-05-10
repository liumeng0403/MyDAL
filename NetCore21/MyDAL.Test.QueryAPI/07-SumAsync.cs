using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _07_SumAsync
        : TestBase
    {
        [Fact]
        public async Task Sum_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
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

            var res1 = await Conn
                .Queryer<AgentInventoryRecord>()
                .Where(it => it.Id != Guid.Parse("df2b788e-6b1a-4a74-ac1d-016551f76dc9"))
                .SumAsync(it => it.TotalSaleCount);

            Assert.True(res1 == 589);

            

            xx = string.Empty;
        }

    }
}
