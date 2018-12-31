using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Delete
{
    public class _02_DeleteTest : TestBase
    {
        private async Task<BodyFitRecord> PreDelete()
        {
            xx = string.Empty;

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

            xx = string.Empty;

            // where 
            var res1 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();
            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            xx = string.Empty;

            var path = "~00-c-1-2-1-1-1-1-1-4-1-1-1-4-1-2-1-7";
            var level = 2;
            // where and
            var res3 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res3 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            xx=string.Empty;

            // where or
            var res2 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .Or(it => it.AgentLevel == (AgentLevel)level)
                .DeleteAsync();
            Assert.True(res2 == 28063);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var xx4 = string.Empty;

            // where and or
            var res4 = await Conn
                .Deleter<Agent>()
                .Where(it => it.PathId == path)
                .And(it => it.AgentLevel == (AgentLevel)level)
                .Or(it => it.CreatedOn >= WhereTest.StartTime)
                .DeleteAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
