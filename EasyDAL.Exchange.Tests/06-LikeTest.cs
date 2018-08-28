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

    }
}
