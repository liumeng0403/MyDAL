using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _09_SumAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var res1 = await Conn
                .Selecter<AlipayPaymentRecord>()
                .Where(it => it.CreatedOn > WhereTest.CreatedOn)
                .SumAsync(it => it.TotalAmount);
            Assert.True(res1 == 1527.2600000000000000000000000M);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var xx = "";
        }
    }
}
