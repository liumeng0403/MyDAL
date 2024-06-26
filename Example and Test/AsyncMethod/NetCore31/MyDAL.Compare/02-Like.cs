﻿using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using Xunit;

namespace MyDAL.Compare
{
    public class _02_Like
        : TestBase
    {

        private BodyFitRecord Pre01()
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
            var resx1 = MyDAL_TestDB.SelectOne<BodyFitRecord>(it => it.Id == m.Id);
            if (resx1 != null)
            {
                var resx2 = MyDAL_TestDB.Delete<BodyFitRecord>(it => it.Id == resx1.Id);
            }

            // 新建
            var res0 = MyDAL_TestDB.Insert(m);

            return m;
        }
        private Agent Pre02()
        {

            xx = string.Empty;

            // 造数据
            var pk1 = Guid.Parse("014c55c3-b371-433c-abc0-016544491da8");
            var resx1 = MyDAL_TestDB.SelectOne<Agent>(it => it.Id == pk1);
            var resx2 = MyDAL_TestDB.Update<Agent>(it => it.Id == resx1.Id, new
            {
                Name = "刘%华"
            });
            var pk3 = Guid.Parse("018a1855-e238-4fb7-82d6-0165442fd654");
            var resx3 = MyDAL_TestDB.SelectOne<Agent>(it => it.Id == pk3);
            var resx4 = MyDAL_TestDB.Update<Agent>(it => it.Id == resx3.Id, new
            {
                Name = "何_伟"
            });

            return resx1;

        }

        [Fact]
        public void History()
        {

            /************************************************************************************************************/

            Pre01();

            xx = string.Empty;

            // 默认 "%"+"xx"+"%"
            var res1 = MyDAL_TestDB.SelectOne<BodyFitRecord>(it => it.BodyMeasureProperty.Contains("xx"));
            Assert.NotNull(res1);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // 默认 "%"+"~00-d-3-1-"+"%"
            var res3 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                    .And(it => it.PathId.Contains("~00-d-3-1-"))
                .SelectPaging(1, 10);
            Assert.True(res3.TotalCount == 5680);

            

            /************************************************************************************************************/

        }

        [Fact]
        public void Like_Shortcut()
        {

            xx = string.Empty;

            // like
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.Contains("陈"));

            Assert.True(res1.Count == 1431);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void NotLike_Shortcut()
        {
            xx = string.Empty;

            // not like
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => !it.Name.Contains("刘"));

            Assert.True(res1.Count == 27159);

            

            /***************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public void Like_ST()
        {

            xx = string.Empty;

            // like 
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                    .And(it => it.PathId.Contains("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res1.TotalCount == 5680);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void NotLike_ST()
        {

            xx = string.Empty;

            // not like 
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !it.PathId.Contains("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res1.TotalCount == 22940);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void Like_MT()
        {

            xx = string.Empty;

            // like
            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.Name.Contains("陈"))
                .SelectList<AgentInventoryRecord>();

            Assert.True(res1.Count == 24);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void NotLike_MT()
        {

            xx = string.Empty;

            // not like
            var res1 = MyDAL_TestDB
                .Selecter(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => !agent1.Name.Contains("陈"))
                .SelectList<AgentInventoryRecord>();

            Assert.True(res1.Count == 550);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void String_StartsWith()
        {

            xx = string.Empty;

            // like StartsWith
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.StartsWith("陈"));

            Assert.True(res1.Count == 1421);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

            // like StartsWith
            var res12 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.PathId.StartsWith("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res12.TotalCount == 5680);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // like StartsWith
            var res13 = MyDAL_TestDB
                .Selecter(out Agent agent13, out AgentInventoryRecord record13)
                .From(() => agent13)
                    .InnerJoin(() => record13)
                        .On(() => agent13.Id == record13.AgentId)
                .Where(() => agent13.Name.StartsWith("张"))
                .SelectList<Agent>();

            Assert.True(res13.Count == 45);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res2 = MyDAL_TestDB.SelectList<Agent>(it => !it.Name.StartsWith("刘"));

            Assert.True(res2.Count == 27163);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res21 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !it.PathId.StartsWith("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res21.TotalCount == 22940);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // not like StartsWith
            var res22 = MyDAL_TestDB
                .Selecter(out Agent agent22, out AgentInventoryRecord record22)
                .From(() => agent22)
                    .InnerJoin(() => record22)
                        .On(() => agent22.Id == record22.AgentId)
                .Where(() => !agent22.Name.StartsWith("张"))
                .SelectList<Agent>();

            Assert.True(res22.Count == 529);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void String_EndsWith()
        {

            /***************************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.EndsWith("陈"));

            Assert.True(res1.Count == 2);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res12 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.PathId.EndsWith("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res12.TotalCount == 0);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // like EndsWith
            var res13 = MyDAL_TestDB
                .Selecter(out Agent agent13, out AgentInventoryRecord record13)
                .From(() => agent13)
                    .InnerJoin(() => record13)
                        .On(() => agent13.Id == record13.AgentId)
                .Where(() => agent13.Name.EndsWith("华"))
                .SelectList<Agent>();

            Assert.True(res13.Count == 22);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res2 = MyDAL_TestDB.SelectList<Agent>(it => !it.Name.EndsWith("刘"));

            Assert.True(res2.Count == 28620);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res21 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !it.PathId.EndsWith("~00-d-3-1-"))
                .SelectPaging(1, 10);

            Assert.True(res21.TotalCount == 28620);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // not like EndsWith
            var res22 = MyDAL_TestDB
                .Selecter(out Agent agent22, out AgentInventoryRecord record22)
                .From(() => agent22)
                    .InnerJoin(() => record22)
                        .On(() => agent22.Id == record22.AgentId)
                .Where(() => !agent22.Name.EndsWith("华"))
                .SelectList<Agent>();

            Assert.True(res22.Count == 552);

            

            /************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void MySQL_通配符()
        {
            xx = string.Empty;

            // %(百分号):  "陈%" --> "陈%"
            var res5 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.Contains("陈%"));

            Assert.True(res5.Count == 1421);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

            // _(下划线):  "王_" --> "王_" 
            var res6 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.Contains("王_"));

            Assert.True(res6.Count == 498);

            

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void MySQL_通配符_转义()
        {

            var resx4 = Pre02();

            /************************************************************************************************************/

            xx = string.Empty;

            // /%(百分号转义):  "刘/%_" --> "刘/%_"
            var res7 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Contains("刘/%_"))
                    .And(it => it.Id == resx4.Id)
                    .And(it => it.Name.Contains("%华"))
                    .And(it => it.Name.Contains("%/%%"))
                .SelectList();

            Assert.True(res7.Count == 1);

            

            /************************************************************************************************************/

            xx = string.Empty;

            // /_(下划线转义):  "何/__" --> "何/__"
            var res8 = MyDAL_TestDB.SelectList<Agent>(it => it.Name.Contains("何/__"));

            Assert.True(res8.Count == 1);

            

            /************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
