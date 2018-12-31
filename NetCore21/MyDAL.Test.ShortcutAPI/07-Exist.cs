using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _07_Exist:TestBase
    {
        [Fact]
        public void Test()
        {
            xx = string.Empty;

            var date = DateTime.Parse("2018-08-20 20:33:21.584925");
            var id = Guid.Parse("89c9407f-7427-4570-92b7-0165590ac07e");
            var res2 = Conn.Exist<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);
            Assert.True(res2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
