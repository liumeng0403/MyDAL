using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Delete
{
    public class _03_QuickApi:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx14 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var res14 = await Conn.DeleteAsync<AlipayPaymentRecord>(pk);
            Assert.True(res14 == 1);

            var tuple14 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res141 = await Conn.GetAsync<AlipayPaymentRecord>(pk);
            Assert.Null(res141);

            /****************************************************************************************/

        }
    }
}
