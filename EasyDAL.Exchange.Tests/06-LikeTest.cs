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
            var xx1 = "";

            // 默认 "%"+"xx"+"%"
            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty.Contains("xx"))
                .QueryFirstOrDefaultAsync();

            var xx = "";
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
