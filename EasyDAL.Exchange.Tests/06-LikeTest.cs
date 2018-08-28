using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class LikeTest:TestBase
    {

        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest()
        {
            var xx3 = "";

            // like string
            var res4 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty.Contains("xx"))
                .QueryFirstOrDefaultAsync();

            var xx = "";
        }

        [Fact]
        public async Task QueryPagingListAsyncTest()
        {
            var xx0 = "";

            // where and like 
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .And(it => it.PathId.Contains(testH.ContainStr))
                .QueryPagingListAsync(1, 10);

            var xx1 = "";

            // where and like 
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .And(it => it.PathId.Contains("~00-d-3-1-"))
                .QueryPagingListAsync(1, 10);

            var xx = "";
        }

    }
}
