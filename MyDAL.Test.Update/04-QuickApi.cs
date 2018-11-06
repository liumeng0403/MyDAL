using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Update
{
    public class _04_QuickApi:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            var xx13 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var res13 = await Conn.UpdateAsync<AlipayPaymentRecord>(pk, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res13 == 1);

            var tuple13 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res131 = await Conn.GetAsync<AlipayPaymentRecord>(pk);
            Assert.True(res131.Description == "xxxxxx");

            /****************************************************************************************/

        }
    }
}
