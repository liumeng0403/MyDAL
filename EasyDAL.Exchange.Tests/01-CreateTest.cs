using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class CreateTest : TestBase
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

            var xx1 = "";

            // 新建
            var res1 = await Conn.OpenHint()
                .CreateAsync<BodyFitRecord>(m);
            //.CreateAsync(m);

            var tuple = (Hints.SQL, Hints.Parameters);

            var xx = "";
        }

        // 创建一个具有可空属性字段的对象
        [Fact]
        public async Task CreateAsyncTest2()
        {

            var m1 = new Agent
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                PathId = "x-xx-xxx-xxxx",
                Name = "张三",
                Phone = "18088889999",
                IdCardNo = "No.12345",
                CrmUserId = "yyyyy",
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,   // DateTime?
                ActiveOrderId  = null,  // Guid?
                DirectorStarCount = 5
            };

            var xx1 = "";

            var res1 = await Conn.OpenHint()
                .CreateAsync(m1);

            var tuple = (Hints.SQL, Hints.Parameters);

            var xx = "";
        }

    }
}
