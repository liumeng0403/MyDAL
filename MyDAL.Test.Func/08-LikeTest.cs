using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _08_LikeTest : TestBase
    {
        private async Task<BodyFitRecord> Pre01()
        {
            // 造数据
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };

            // 清理数据
            var resx1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .QueryFirstOrDefaultAsync();
            if (resx1 != null)
            {
                var resx2 = await Conn
                    .Deleter<BodyFitRecord>()
                    .Where(it => it.Id == resx1.Id)
                    .DeleteAsync();
            }

            // 新建
            var res0 = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            return m;
        }
        private async Task<Agent> Pre02()
        {

            var xxx = "";

            // 造数据
            var resx1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("014c55c3-b371-433c-abc0-016544491da8"))
                .QueryFirstOrDefaultAsync();
            var resx2 = await Conn
                .Updater<Agent>()
                .Set(it => it.Name, "刘%华")
                .Where(it => it.Id == resx1.Id)
                .UpdateAsync();
            var resx3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("018a1855-e238-4fb7-82d6-0165442fd654"))
                .QueryFirstOrDefaultAsync();
            var resx4 = await Conn
                .Updater<Agent>()
                .Set(it => it.Name, "何_伟")
                .Where(it => it.Id == resx3.Id)
                .UpdateAsync();

            return resx1;

        }

        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest()
        {

            /************************************************************************************************************/

            var m = await Pre01();

            var xx1 = "";

            // 默认 "%"+"xx"+"%"
            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty.Contains("xx"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx2 = "";

            // testH.ContainStr="~00-d-3-1-"
            // 默认 "%"+testH.ContainStr+"%"
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .And(it => it.PathId.Contains(WhereTest.ContainStr))
                .QueryPagingListAsync(1, 10);
            Assert.True(res2.TotalCount == 5680);

            var sql2 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx3 = "";

            // 默认 "%"+"~00-d-3-1-"+"%"
            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .And(it => it.PathId.Contains("~00-d-3-1-"))
                .QueryPagingListAsync(1, 10);
            Assert.True(res3.TotalCount == 5680);

            var sql3 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var resx4 = await Pre02();

            var xx4 = "";

            // 无通配符 -- "陈" -- "%"+"陈"+"%"
            var res0 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.无通配符))
                .QueryListAsync();
            Assert.True(res0.Count == 1431);

            var sql4 = (XDebug.SQL, XDebug.Parameters);

            var xx5 = "";

            // 百分号 -- "陈%" -- "陈%"
            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .QueryListAsync();
            Assert.True(res5.Count == 1421);

            var sql5 = (XDebug.SQL, XDebug.Parameters);

            var xx6 = "";

            // 下划线 -- "王_" -- "王_" 
            var res6 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.下划线))
                .QueryListAsync();
            Assert.True(res6.Count == 498);

            var sql6 = (XDebug.SQL, XDebug.Parameters);

            var xx7 = "";

            // 百分号转义 -- "刘/%_" -- "刘/%_"
            var res7 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号转义))
                .And(it => it.Id == resx4.Id)
                .And(it => it.Name.Contains("%华"))
                .And(it => it.Name.Contains("%/%%"))
                .QueryListAsync();
            Assert.True(res7.Count == 1);

            var sql7 = (XDebug.SQL, XDebug.Parameters);

            var xx8 = "";

            // 下划线转义 -- "何/__" -- "何/__"
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.下划线转义))
                .QueryListAsync();
            Assert.True(res4.Count == 1);

            var sql8 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx9 = "";

            var res9 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                    .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.Name.Contains(LikeTest.无通配符))
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res9.Count == 24);

            var sql9 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx10 = "";

            var res10 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent10, out var record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                    .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.Name.StartsWith("张"))
                .QueryListAsync<Agent>();
            Assert.True(res10.Count == 45);

            var sql10 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx11 = "";

            var res11 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent11, out var record11)
                .From(() => agent11)
                .InnerJoin(() => record11)
                .On(() => agent11.Id == record11.AgentId)
                .Where(() => agent11.Name.EndsWith("华"))
                .QueryListAsync<Agent>();
            Assert.True(res11.Count == 22);

            var sql11 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************/

            var xx = "";

        }

    }
}
