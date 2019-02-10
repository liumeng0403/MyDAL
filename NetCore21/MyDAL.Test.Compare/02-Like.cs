﻿using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _02_Like
        : TestBase
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
        public async Task Test01()
        {

            /************************************************************************************************************/

            await Pre01();

            xx = string.Empty;

            // 默认 "%"+"xx"+"%"
            var res1 = await Conn.FirstOrDefaultAsync<BodyFitRecord>(it => it.BodyMeasureProperty.Contains("xx"));
            Assert.NotNull(res1);

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

        }

        [Fact]
        public async Task Like_Shortcut()
        {

            xx = string.Empty;

            // like
            var res1 = await Conn.QueryListAsync<Agent>(it => it.Name.Contains("陈"));

            Assert.True(res1.Count == 1431);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // like StartsWith
            var res2 = await Conn.QueryListAsync<Agent>(it => it.Name.StartsWith("陈"));

            Assert.True(res2.Count == 1421);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res3 = await Conn.QueryListAsync<Agent>(it => it.Name.EndsWith("陈"));

            Assert.True(res3.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotLike_Shortcut()
        {
            xx = string.Empty;

            // not like
            var res1 = await Conn.QueryListAsync<Agent>(it => !it.Name.Contains("刘"));

            Assert.True(res1.Count == 27159);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res2 = await Conn.QueryListAsync<Agent>(it => !it.Name.StartsWith("刘"));

            Assert.True(res2.Count == 27163);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res3 = await Conn.QueryListAsync<Agent>(it => !it.Name.EndsWith("刘"));

            Assert.True(res3.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Like_ST()
        {

            xx = string.Empty;

            // like 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                    .And(it => it.PathId.Contains("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res1.TotalCount == 5680);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // like StartsWith
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.PathId.StartsWith("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res2.TotalCount == 5680);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.PathId.EndsWith("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res3.TotalCount == 0);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotLike_ST()
        {

            xx = string.Empty;

            // not like 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => !it.PathId.Contains("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res1.TotalCount == 22940);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => !it.PathId.StartsWith("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res2.TotalCount == 22940);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => !it.PathId.EndsWith("~00-d-3-1-"))
                .PagingListAsync(1, 10);

            Assert.True(res3.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Like_MT()
        {

            xx = string.Empty;

            // like
            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.Name.Contains("陈"))
                .QueryListAsync<AgentInventoryRecord>();

            Assert.True(res1.Count == 24);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // like StartsWith
            var res2 = await Conn
                .Queryer(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name.StartsWith("张"))
                .QueryListAsync<Agent>();

            Assert.True(res2.Count == 45);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.EndsWith("华"))
                .QueryListAsync<Agent>();

            Assert.True(res3.Count == 22);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task NotLike_MT()
        {

            xx = string.Empty;

            // not like
            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => !agent1.Name.Contains("陈"))
                .QueryListAsync<AgentInventoryRecord>();

            Assert.True(res1.Count == 550);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res2 = await Conn
                .Queryer(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => !agent2.Name.StartsWith("张"))
                .QueryListAsync<Agent>();

            Assert.True(res2.Count == 529);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => !agent3.Name.EndsWith("华"))
                .QueryListAsync<Agent>();

            Assert.True(res3.Count == 552);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task String_StartsWith()
        {

        }

        [Fact]
        public async Task String_EndsWith()
        {

        }

        [Fact]
        public async Task MySQL_通配符()
        {

        }

        [Fact]
        public async Task MySQL_通配符_转义()
        {

        }

    }
}
