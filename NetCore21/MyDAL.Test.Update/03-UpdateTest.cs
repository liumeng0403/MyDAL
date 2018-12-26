using MyDAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Update
{
    public class _03_UpdateTest : TestBase
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
            var res2 = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            return m;

        }

        // 修改一个已有对象
        [Fact]
        public async Task UpdateAsyncTest()
        {
            var xx = string.Empty;
            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var m = await CreateDbData();
            var Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e");
            var m2 = 10;
            var id2 = Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e");

            /***************************************************************************************************************************/

            xx = string.Empty;

            // DB data
            var m1 = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,   // new value
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn,zzz:aaa}"   // new value
            };

            // 修改

            // set field 1
            var res1 = await Conn
                .Updater<BodyFitRecord>()
                .Set(it => it.CreatedOn, m1.CreatedOn)
                .Set(it => it.BodyMeasureProperty, m1.BodyMeasureProperty)
                .Where(it => it.Id == m.Id)
                .UpdateAsync();
            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // set field 2
            var res2 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(it => it.LockedCount, 100)
                .Where(it => it.AgentId == id2)
                .Or(it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074"))
                .UpdateAsync();
            Assert.True(res2 == 2);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // set dynamic
            var res3 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(new
                {
                    TotalSaleCount = 1000,
                    xxx = 2000
                })
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();
            Assert.True(res3 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            dynamic obj = new ExpandoObject();
            obj.TotalSaleCount = 2000;
            obj.xxx = 3000;
            // set expand object
            var res4 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(obj as object)
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();
            Assert.True(res4 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // where change
            var res5 = await Conn
                .Updater<AgentInventoryRecord>()
                .Change(it => it.LockedCount, m2, ChangeEnum.Add)
                .Where(it => it.AgentId == id2)
                .And(it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109"))
                .UpdateAsync();
            Assert.True(res5 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var resx6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .FirstOrDefaultAsync();
            resx6.ActivedOn = null;

            // update set null
            var res6 = await Conn
                .Updater<Agent>()
                .Set(it => it.ActivedOn, resx6.ActivedOn)
                .Where(it => it.Id == resx6.Id)
                .UpdateAsync();
            Assert.True(res6 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

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
                .FirstOrDefaultAsync();
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

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resxx7 = await Conn
                .Queryer<Product>()
                .Where(it => it.Id == guid7)
                .FirstOrDefaultAsync();
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
                .FirstOrDefaultAsync();
            Assert.True(res81.AgentLevel == AgentLevel.NewCustomer);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
