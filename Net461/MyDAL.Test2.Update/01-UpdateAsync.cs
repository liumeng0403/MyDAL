using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;

namespace MyDAL.Test2.Update
{
    [TestClass]
    public class _01_UpdateAsync
        :TestBase
    {
        [TestMethod]
        public async Task Update_Shortcut()
        {
            xx = string.Empty;

            var pk1 = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");

            // 修改一条数据: AlipayPaymentRecord
            var res1 = await Conn2.UpdateAsync<AlipayPaymentRecord>(it => it.Id == pk1,     //  where 条件: it=>it.Id==pk1
            new
            {
                Description = "new desc",    // 修改 AlipayPaymentRecord 字段 Description 的值为: "new desc"
                PaymentUrl = "new url"    //  修改 AlipayPaymentRecord 字段 PaymentUrl 的值为: "new url"
            });

            Assert.IsTrue(res1 == 1);

            

            // 查询一条数据: AlipayPaymentRecord
            var res11 = await Conn2.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk1);
            Assert.IsTrue(res11.Description == "new desc");

            /****************************************************************************************/

            xx = string.Empty;
        }
    }
}
