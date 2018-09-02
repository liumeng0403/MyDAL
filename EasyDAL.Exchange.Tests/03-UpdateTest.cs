using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.Core.Sql;
using System.Dynamic;

namespace EasyDAL.Exchange.Tests
{
    public class UpdateTest:TestBase
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
                .CreateAsync<BodyFitRecord>(m);
                //.CreateAsync(m);

            return m;

        }

        // 修改一个已有对象
        [Fact]
        public async Task UpdateAsyncTest()
        {
            var xx0 = "";

            var m = await CreateDbData();
            var Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e");
            var m2 = 10;
            var id2 = Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e");

            var xx1 = "";

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
            var res1 = await Conn.OpenHint()
                .Updater<BodyFitRecord>()      
                .Set(it => it.CreatedOn, m1.CreatedOn)
                .Set(it => it.BodyMeasureProperty, m1.BodyMeasureProperty)
                .Where(it => it.Id == m.Id)
                .UpdateAsync();

            var tuple1 = (Hints.SQL, Hints.Parameters);

            var xx2 = "";

            // set field 2
            var res2 = await Conn
                .Updater<AgentInventoryRecord>()
                .Set(it => it.LockedCount, 100)
                .Where(it => it.AgentId == id2)
                .Or(it => it.CreatedOn == Convert.ToDateTime("2018-08-19 11:34:42.577074"))
                .UpdateAsync();

            var tuple2 = (Hints.SQL, Hints.Parameters);

            var xx3 = "";

            // set dynamic
            var res3 = await Conn.OpenHint()
                .Updater<AgentInventoryRecord>()
                .Set(new
                {
                    TotalSaleCount = 1000,
                    xxx = 2000
                })
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();

            var tuple3 = (Hints.SQL, Hints.Parameters);


            var xx4 = "";

            dynamic obj = new ExpandoObject();
            obj.TotalSaleCount = 2000;
            obj.xxx = 3000;
            // set expand object
            var res4 = await Conn.OpenHint()
                .Updater<AgentInventoryRecord>()
                .Set(obj as object)
                .Where(it => it.Id == Guid.Parse("032ce51f-1034-4fb2-9741-01655202ecbc"))
                .UpdateAsync();

            var tuple4 = (Hints.SQL, Hints.Parameters);

            var xx5 = "";

            // where change
            var res5 = await Conn
                .Updater<AgentInventoryRecord>()
                .Change(it => it.LockedCount, m2, ChangeEnum.Add)
                .Where(it => it.AgentId == id2)
                .And(it => it.ProductId == Guid.Parse("85ce17c1-10d9-4784-b054-016551e5e109"))
                .UpdateAsync();

            var tuple5 = (Hints.SQL, Hints.Parameters);

            var xx6 = "";

            var resx6 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync();
            resx6.ActivedOn = null;

            // update set null
            var res6 = await Conn
                .Updater<Agent>()
                .Set(it => it.ActivedOn, resx6.ActivedOn)
                .Where(it => it.Id == resx6.Id)
                .UpdateAsync();

            var tuple6 = (Hints.SQL, Hints.Parameters);

            var xx = "";
        }

    }
}
