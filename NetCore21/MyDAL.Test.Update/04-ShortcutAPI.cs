using MyDAL.Test.Entities.MyDAL_TestDB;
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

            xx = string.Empty;

            var pk1 = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");

            // 修改一条数据: AlipayPaymentRecord
            var res1 = await Conn.UpdateAsync<AlipayPaymentRecord>(it=>it.Id==pk1, new    //  where 条件: it=>it.Id==pk1
            {
                Description = "new desc",    // 修改 AlipayPaymentRecord 字段 Description 的值为: "new desc"
                PaymentUrl = "new url"    //  修改 AlipayPaymentRecord 字段 PaymentUrl 的值为: "new url"
            });
            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            // 查询一条数据: AlipayPaymentRecord
            var res11 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it=>it.Id==pk1);

            Assert.True(res11.Description == "new desc");

            /****************************************************************************************/

            xx = string.Empty;

            var pk2 = Guid.Parse("d0a2d3f3-5cfb-4b3b-aeea-016557383999");
            var res2 = await Conn.UpdateAsync<AlipayPaymentRecord>(it => it.Id == pk2, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res2 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res21 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it=>it.Id==pk2);

            Assert.True(res21.Description == "xxxxxx");

            /****************************************************************************************/

            xx=string.Empty;

        }
    }
}
