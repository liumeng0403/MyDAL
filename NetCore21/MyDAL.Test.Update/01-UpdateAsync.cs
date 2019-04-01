using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Update
{
    public class _01_UpdateAsync
        : TestBase
    {
        private async Task<BodyFitRecord> CreateDbData()
        {
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };

            // 删
            var res1 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            // 建
            var res2 = await Conn.CreateAsync(m);

            return m;

        }

        [Fact]
        public async Task History_01()
        {
            xx = string.Empty;

            var pk2 = Guid.Parse("d0a2d3f3-5cfb-4b3b-aeea-016557383999");
            var res2 = await Conn.UpdateAsync<AlipayPaymentRecord>(it => it.Id == pk2, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res2 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res21 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk2);

            Assert.True(res21.Description == "xxxxxx");

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_Shortcut()
        {

            xx = string.Empty;

            var pk1 = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");

            // 修改一条数据: AlipayPaymentRecord
            var res1 = await Conn.UpdateAsync<AlipayPaymentRecord>(it => it.Id == pk1,     //  where 条件: it=>it.Id==pk1
            new
            {
                Description = "new desc",    // 修改 AlipayPaymentRecord 字段 Description 的值为: "new desc"
                PaymentUrl = "new url"    //  修改 AlipayPaymentRecord 字段 PaymentUrl 的值为: "new url"
            });

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            // 查询一条数据: AlipayPaymentRecord
            var res11 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk1);
            Assert.True(res11.Description == "new desc");

            /****************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Update_SetField_ST()
        {
            xx = string.Empty;

            var m = await CreateDbData();

            // set field 1
            var res1 = await Conn
                .Updater<BodyFitRecord>()
                .Set(it => it.CreatedOn, DateTime.Now)
                .Set(it => it.BodyMeasureProperty, "{xxx:yyy,mmm:nnn,zzz:aaa}")
                .Where(it => it.Id == m.Id)
                .UpdateAsync();

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_ChangeAdd_ST()
        {

        }

        [Fact]
        public async Task Update_SetField_ChangeMinus_ST()
        {

        }

        [Fact]
        public async Task Update_SetObject_ST()
        {

        }

        [Fact]
        public async Task Update_SetDynamic_ST()
        {

        }

        [Fact]
        public async Task Update_MT()
        {

        }
    }
}
