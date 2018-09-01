using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using EasyDAL.Exchange.Tests.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class PagingTest : TestBase
    {

        // 分页查询 单条件
        [Fact]
        public async Task QueryPagingListAsyncTest()
        {
            var xx0 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .QueryPagingListAsync(1, 10);

            var xx = "";
        }


        // 分页查询 多条件
        [Fact]
        public async Task QueryPagingListAsyncVMTest()
        {
            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var xx = "";
        }

    }
}
