using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _09_SumAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AlipayPaymentRecord>()
                .Where(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SumAsync(it => it.TotalAmount);

            Assert.True(res1 == 1527.2600000000000000000000000M);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
