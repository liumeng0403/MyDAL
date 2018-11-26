using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Delete
{
    public class _03_QuickApi : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx1 = "";

            var pk1 = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var res1 = await Conn.DeleteAsync<AlipayPaymentRecord>(pk1);
            Assert.True(res1 == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.GetAsync<AlipayPaymentRecord>(pk1);
            Assert.Null(res11);

            /****************************************************************************************/

            var xx2 = "";

            var pk2 = Guid.Parse("72d551bf-d9f4-4817-800f-01655794cf42");
            var res2 = await Conn.DeleteAsync<AlipayPaymentRecord>(it => it.Id == pk2);
            Assert.True(res2 == 1);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res21 = await Conn.GetAsync<AlipayPaymentRecord>(pk2);
            Assert.Null(res21);

            /****************************************************************************************/

            var xx = "";
        }
    }
}
