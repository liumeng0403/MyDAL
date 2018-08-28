using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class PagingTest:TestBase
    {

        // 分页查询 单条件
        [Fact]
        public async Task QueryPagingListAsyncTest()
        {
            var xx0 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .QueryPagingListAsync(1,10);

            var xx = "";
        }


        // 分页查询 多条件
        [Fact]
        public async Task QueryPagingListAsyncTest2()
        {
            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-10),
                StartTime = DateTime.Now.AddDays(-10),
                EndTime = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };


            var xx = "";
        }

    }
}
