using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class CreateTest:TestBase
    {
        private async Task PreCreate(BodyFitRecord m)
        {
            // 清除数据
            var xx1 = "";

            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .QueryFirstOrDefaultAsync();

            var xx2 = "";

            var res2 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == res1.Id)
                .DeleteAsync();
        }

        // 创建一个新对象
        [Fact]
        public async Task CreateAsyncTest()
        {
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            await PreCreate(m);

            var xx0 = "";

            // 新建
            var res0 = await Conn.OpenHint()
                .CreateAsync<BodyFitRecord>(m);
            //.CreateAsync(m);

            var tuple = (Hints.SQL, Hints.Parameters);

            var xx = "";
        }
    }
}
