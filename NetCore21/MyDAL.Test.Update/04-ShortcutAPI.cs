using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Update
{
    public class _04_QuickApi : TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            var xx1 = "";

            var pk1 = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var res1 = await Conn.UpdateAsync<AlipayPaymentRecord>(it=>it.Id==pk1, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res1 == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(it=>it.Id==pk1);
            Assert.True(res11.Description == "xxxxxx");

            /****************************************************************************************/

            var xx2 = "";

            var pk2 = Guid.Parse("d0a2d3f3-5cfb-4b3b-aeea-016557383999");
            var res2 = await Conn.UpdateAsync<AlipayPaymentRecord>(it => it.Id == pk2, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res2 == 1);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res21 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(it=>it.Id==pk2);
            Assert.True(res21.Description == "xxxxxx");

            /****************************************************************************************/

            var xx = "";

        }
    }
}
