using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _07_ExistAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var date = DateTime.Parse("2018-08-20 20:33:21.584925");
            var id = Guid.Parse("89c9407f-7427-4570-92b7-0165590ac07e");

            // 判断 AlipayPaymentRecord 表中是否存在符合条件的数据
            bool res1 = await Conn.ExistAsync<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);
            Assert.True(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
