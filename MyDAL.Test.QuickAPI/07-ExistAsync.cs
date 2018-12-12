using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuickAPI
{
    public class _08_ExistAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var date = DateTime.Parse("2018-08-20 20:33:21.584925");
            var id = Guid.Parse("89c9407f-7427-4570-92b7-0165590ac07e");
            var res1 = await Conn.ExistAsync<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);
            Assert.True(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            var xx2 = "";
            
            var res2 = Conn.Exist<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);
            Assert.True(res2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            var xx = "";
        }
    }
}
