using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _01_CreateTest : TestBase
    {
        private async Task PreCreate(BodyFitRecord m)
        {
            // 清除数据

            xx = string.Empty;

            var res2 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .DeleteAsync();
        }
        private async Task ClearData6()
        {
            await Conn
                .Deleter<Agent>()
                .Where(it => it.Id == Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"))
                .DeleteAsync();
        }
        private async Task ClearData7()
        {
            await Conn
                .Deleter<Agent>()
                .Where(it => it.Id == Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"))
                .DeleteAsync();
        }

        // 创建一个新对象
        [Fact]
        public async Task CreateAsyncTest()
        {

            /********************************************************************************************************************************/

            var m1 = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            await PreCreate(m1);

            xx = string.Empty;

            // 新建
            var res1 = await Conn.CreateAsync(m1);

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var m2 = new Agent
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
                ActiveOrderId = null,  // Guid?
                DirectorStarCount = 5
            };

            xx = string.Empty;

            var res2 = await Conn.CreateAsync(m2);

            Assert.True(res2 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx5 = string.Empty;

            var res5 = await Conn.CreateAsync(new Agent
            {
                Id = Guid.NewGuid(),
                CreatedOn = Convert.ToDateTime("2018-10-07 17:02:05"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = null,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            });

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx6 = string.Empty;


            await ClearData6();
            var m6 = new Agent
            {
                Id = Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"),
                CreatedOn = Convert.ToDateTime("2018-10-07 17:02:05"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            };
            var res6 = await Conn.CreateAsync(m6);

            var res61 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"))
                .QueryOneAsync<Agent>();

            Assert.True(res61.AgentLevel == AgentLevel.DistiAgent);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx7 = string.Empty;

            await ClearData7();
            var m7 = new Agent
            {
                Id = Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"),
                CreatedOn = Convert.ToDateTime("2018-08-16 19:34:25.116759"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            };

            var res7 = await Conn.CreateAsync(m7);

            var res71 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"))
                .QueryOneAsync<Agent>();

            Assert.True(res71.CreatedOn == Convert.ToDateTime("2018-08-16 19:34:25.116759"));

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
