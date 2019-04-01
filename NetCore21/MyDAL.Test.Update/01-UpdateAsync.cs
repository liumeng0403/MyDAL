using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Dynamic;
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
        public async Task History_02()
        {
            xx = string.Empty;

            // set field 2
            var res2 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(it => it.LockedCount, 100)
                .Where(it => it.AgentId == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .Or(it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074"))
                .UpdateAsync();

            Assert.True(res2 == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var resx6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync();

            resx6.ActivedOn = null;

            // update set null
            var res6 = await Conn
                .Updater<Agent>()
                .Set(it => it.ActivedOn, resx6.ActivedOn)
                .Where(it => it.Id == resx6.Id)
                .UpdateAsync();

            Assert.True(res6 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var guid7 = Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f");

            var resxxx7 = await Conn
                .Updater<Product>()
                .Set(it => it.VipProduct, false)
                .Where(it => it.Id == guid7)
                .UpdateAsync();

            var resx7 = await Conn
                .Queryer<Product>()
                .Where(it => it.Id == guid7)
                .QueryOneAsync();

            Assert.NotNull(resx7);
            Assert.False(resx7.VipProduct);

            resx7.VipProduct = true;

            // update set bool bit
            var res7 = await Conn
                .Updater<Product>()
                .Set(it => it.VipProduct, resx7.VipProduct)
                .Where(it => it.Id == resx7.Id)
                .UpdateAsync();

            Assert.True(res7 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resxx7 = await Conn
                .Queryer<Product>()
                .Where(it => it.Id == guid7)
                .QueryOneAsync();

            Assert.True(resxx7.VipProduct);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res8 = await Conn
                .Updater<Agent>()
                .Set(it => it.AgentLevel, AgentLevel.NewCustomer)
                .Where(it => it.Id == Guid.Parse("0014f62d-2a96-4b5b-b4bd-01654438e3d4"))
                .UpdateAsync();

            var res81 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("0014f62d-2a96-4b5b-b4bd-01654438e3d4"))
                .QueryOneAsync();

            Assert.True(res81.AgentLevel == AgentLevel.NewCustomer);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

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
        public async Task Update_SetField_AllowedNull_Shortcut()
        {

            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));
            agent.PathId = null;

            var res1 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
            {
                agent.PathId
            });

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Null(res11.PathId);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_NotAllowedNull_Shortcut()
        {
            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));
            agent.PathId = null;

            try
            {
                var res1 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
                {
                    agent.PathId
                }, SetEnum.NotAllowedNull);
            }
            catch (Exception ex)
            {
                Assert.Equal("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", ex.Message, true);
            }

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_IgnoreNull_Shortcut()
        {

            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));
            agent.PathId = "yyyyyyy";
            agent.ActiveOrderId = null;

            var res1 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
            {
                agent.PathId,
                agent.ActiveOrderId
            }, SetEnum.IgnoreNull);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Equal("yyyyyyy", res11.PathId, true);
            Assert.NotNull(res11.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

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
        public async Task Update_SetField_AllowedNull_ST()
        {
            
            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));

            var res1 = await Conn
                .Updater<Agent>()
                .Set(it => it.PathId, null)
                .Where(it => it.Id == agent.Id)
                .UpdateAsync();

            var res11 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Null(res11.PathId);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_NotAllowedNull_ST()
        {

            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));

            try
            {
                var res1 = await Conn
                    .Updater<Agent>()
                    .Set(it => it.PathId, null)
                    .Where(it => it.Id == agent.Id)
                    .UpdateAsync(SetEnum.NotAllowedNull);
            }
            catch (Exception ex)
            {
                Assert.Equal("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", ex.Message, true);
            }

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_IgnoreNull_ST()
        {

            xx = string.Empty;

            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));
            agent.PathId = "xxxxxxx";
            agent.ActiveOrderId = null;

            var res1 = await Conn
                .Updater<Agent>()
                .Set(new
                {
                    agent.PathId,
                    agent.ActiveOrderId
                })
                .Where(it => it.Id == agent.Id)
                .UpdateAsync(SetEnum.IgnoreNull);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Equal("xxxxxxx", res11.PathId, true);
            Assert.NotNull(res11.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Update_SetField_ChangeAdd_ST()
        {

            xx = string.Empty;

            // 
            var res5 = await Conn
                .Updater<AgentInventoryRecord>()
                .Change(it => it.LockedCount, 10, ChangeEnum.Add)
                .Where(it => it.AgentId == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .And(it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109"))
                .UpdateAsync();

            Assert.True(res5 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Update_SetField_ChangeMinus_ST()
        {

            xx = string.Empty;

            // 
            var res5 = await Conn
                .Updater<AgentInventoryRecord>()
                .Change(it => it.LockedCount, 10, ChangeEnum.Minus)
                .Where(it => it.AgentId == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .And(it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109"))
                .UpdateAsync();

            Assert.True(res5 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Update_SetObject_ST()
        {

            xx = string.Empty;

            //
            var res1 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(new
                {
                    TotalSaleCount = 1000,
                    xxx = 2000
                })
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Update_SetDynamic_ST()
        {

            xx = string.Empty;

            dynamic obj = new ExpandoObject();
            obj.TotalSaleCount = 2000;
            obj.xxx = 3000;

            // 
            var res1 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(obj as object)
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();
            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Update_MT()
        {
            /*
             * 多表连接方式更新表数据，自己写 SQL
             * 然后使用方法： IDbConnection.ExecuteNonQueryAsync(string sql, List<XParam> dbParas = null)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
             */

            try
            {
                await None();
            }
            catch { }
        }
    }
}
