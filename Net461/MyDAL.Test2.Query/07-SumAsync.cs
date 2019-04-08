using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _07_SumAsync
        : TestBase
    {

        [TestMethod]
        public async Task Sum_Nullable_ST()
        {
            xx = string.Empty;

            var res1 = await Conn2
                .Queryer<AgentInventoryRecord>()
                .Where(it => it.Id != Guid.Parse("df2b788e-6b1a-4a74-ac1d-016551f76dc9"))
                .SumAsync(it => it.TotalSaleCount);

            Assert.IsTrue(res1 == 589);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

    }
}
