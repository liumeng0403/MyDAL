using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Delete
{
    public class _02_DeleteTest : TestBase
    {
        private async Task<BodyFitRecord> PreDelete()
        {
            var xx0 = "";

            // 造数据 
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };

            var res = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            var res0 = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            return m;
        }

        // 删除 已存在对象
        [Fact]
        public async Task DeleteAsyncTest()
        {
            var m = await PreDelete();

            var xx1 = "";

            // where 
            var res1 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();
            Assert.True(res1 == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            var path = "~00-c-1-2-1-1-1-1-1-4-1-1-1-4-1-2-1-7";
            var level = 2;
            // where and
            var res3 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res3 == 1);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            // where or
            var res2 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .Or(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res2 == 28063);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx4 = "";

            // where and or
            var res4 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .Or(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .DeleteAsync();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }
    }
}
