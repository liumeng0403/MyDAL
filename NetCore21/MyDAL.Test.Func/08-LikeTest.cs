using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
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
            var resx1 = await Conn.FirstOrDefaultAsync<BodyFitRecord>(it => it.Id == m.Id);
            if (resx1 != null)
            {
                var resx2 = await Conn.DeleteAsync<BodyFitRecord>(it => it.Id == resx1.Id);
            }

            // 新建
            var res0 = await Conn.CreateAsync(m);

            return m;
        }
        private async Task<Agent> Pre02()
        {

            xx = string.Empty;

            // 造数据
            var pk1 = Guid.Parse("014c55c3-b371-433c-abc0-016544491da8");
            var resx1 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == pk1);
            var resx2 = await Conn.UpdateAsync<Agent>(it => it.Id == resx1.Id, new
            {
                Name = "刘%华"
            });
            var pk3 = Guid.Parse("018a1855-e238-4fb7-82d6-0165442fd654");
            var resx3 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == pk3);
            var resx4 = await Conn.UpdateAsync<Agent>(it => it.Id == resx3.Id, new
            {
                Name = "何_伟"
            });

            return resx1;

        }

        [Fact]
        public async Task FirstOrDefaultAsyncTest()
        {

            /************************************************************************************************************/

            var m = await Pre01();

            xx = string.Empty;

            // 默认 "%"+"xx"+"%"
            var res1 = await Conn.FirstOrDefaultAsync<BodyFitRecord>(it => it.BodyMeasureProperty.Contains("xx"));
            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // testH.ContainStr="~00-d-3-1-"
            // 默认 "%"+testH.ContainStr+"%"
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                    .And(it => it.PathId.Contains(WhereTest.ContainStr))
                .PagingListAsync(1, 10);
            Assert.True(res2.TotalCount == 5680);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // 默认 "%"+"~00-d-3-1-"+"%"
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                    .And(it => it.PathId.Contains("~00-d-3-1-"))
                .PagingListAsync(1, 10);
            Assert.True(res3.TotalCount == 5680);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            var resx4 = await Pre02();

            xx = string.Empty;

            // 无通配符 -- "陈" -- "%"+"陈"+"%"
            var res4 = await Conn.QueryListAsync<Agent>(it => it.Name.Contains(LikeTest.无通配符));
            Assert.True(res4.Count == 1431);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            // 百分号 -- "陈%" -- "陈%"
            var res5 = await Conn.QueryListAsync<Agent>(it => it.Name.Contains(LikeTest.百分号));
            Assert.True(res5.Count == 1421);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            // 下划线 -- "王_" -- "王_" 
            var res6 = await Conn.QueryListAsync<Agent>(it => it.Name.Contains(LikeTest.下划线));
            Assert.True(res6.Count == 498);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            // 百分号转义 -- "刘/%_" -- "刘/%_"
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号转义))
                    .And(it => it.Id == resx4.Id)
                    .And(it => it.Name.Contains("%华"))
                    .And(it => it.Name.Contains("%/%%"))
                .QueryListAsync();
            Assert.True(res7.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            // 下划线转义 -- "何/__" -- "何/__"
            var res8 = await Conn.QueryListAsync<Agent>(it => it.Name.Contains(LikeTest.下划线转义));
            Assert.True(res8.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            var res9 = await Conn
                .Queryer(out Agent agent9, out AgentInventoryRecord record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.Name.Contains(LikeTest.无通配符))
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res9.Count == 24);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            var res10 = await Conn
                .Queryer(out Agent agent10, out AgentInventoryRecord record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.Name.StartsWith("张"))
                .QueryListAsync<Agent>();
            Assert.True(res10.Count == 45);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            var res11 = await Conn
                .Queryer(out Agent agent11, out AgentInventoryRecord record11)
                .From(() => agent11)
                    .InnerJoin(() => record11)
                        .On(() => agent11.Id == record11.AgentId)
                .Where(() => agent11.Name.EndsWith("华"))
                .QueryListAsync<Agent>();
            Assert.True(res11.Count == 22);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
