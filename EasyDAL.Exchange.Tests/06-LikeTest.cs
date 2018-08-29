using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class LikeTest : TestBase
    {

        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest()
        {
            // 造数据
            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            // 新建
            var res0 = await Conn.OpenHint()
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            var xx1 = "";

            // 默认 "%"+"xx"+"%"
            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty.Contains("xx"))
                .QueryFirstOrDefaultAsync();

            var xx = "";

            // 清理数据
            var resx1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .QueryFirstOrDefaultAsync();
            var resx2 = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == res1.Id)
                .DeleteAsync();
        }

        [Fact]
        public async Task QueryPagingListAsyncTest()
        {
            var xx1 = "";

            // testH.ContainStr="~00-d-3-1-"
            // 默认 "%"+testH.ContainStr+"%"
            var res1 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .And(it => it.PathId.Contains(testH.ContainStr))
                .QueryPagingListAsync(1, 10);

            var sql1 = (Hints.SQL, Hints.Parameters);

            var xx2 = "";

            // 默认 "%"+"~00-d-3-1-"+"%"
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .And(it => it.PathId.Contains("~00-d-3-1-"))
                .QueryPagingListAsync(1, 10);

            var sql2 = (Hints.SQL, Hints.Parameters);

            var xx = "";
        }


        [Fact]
        public async Task QueryListAsyncTest()
        {
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

            var xx0 = "";

            // 无通配符 -- "陈" -- "%"+"陈"+"%"
            var res0 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.无通配符))
                .QueryListAsync();

            var sql0 = (Hints.SQL, Hints.Parameters);

            var xx1 = "";

            // 百分号 -- "陈%" -- "陈%"
            var res1 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .QueryListAsync();

            var sql1 = (Hints.SQL, Hints.Parameters);

            var xx2 = "";

            // 下划线 -- "王_" -- "王_" 
            var res2 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.下划线))
                .QueryListAsync();

            var sql2 = (Hints.SQL, Hints.Parameters);

            var xx3 = "";

            // 百分号转义 -- "刘/%_" -- "刘/%_"
            var res3 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号转义))
                .And (it=>it.Id==resx1.Id)
                .And (it=>it.Name.Contains("%华"))
                .And(it=>it.Name.Contains("%/%%"))
                .QueryListAsync();

            var sql3 = (Hints.SQL, Hints.Parameters);

            var xx4 = "";

            // 下划线转义 -- "何/__" -- "何/__"
            var res4 = await Conn.OpenHint()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.下划线转义))
                .QueryListAsync();

            var sql4 = (Hints.SQL, Hints.Parameters);

            var xx = "";

        }

    }
}
