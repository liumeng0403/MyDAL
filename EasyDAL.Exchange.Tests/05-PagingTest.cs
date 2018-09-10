using EasyDAL.Exchange.Core;
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

        // 分页查询 m
        [Fact]
        public async Task QueryPagingListAsyncTest()
        {
            var xx0 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .QueryPagingListAsync(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.DateTime_大于等于 <= it.CreatedOn)
                .QueryPagingListAsync(1, 10);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.TotalCount == resR1.TotalCount);

            var xx = "";
        }


        // 分页查询 m --> vm
        [Fact]
        public async Task QueryPagingListAsyncVMTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => WhereTest.DateTime_大于等于 <= it.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.TotalCount == resR1.TotalCount);

            var xx = "";
        }

        // 分页查询 m
        [Fact]
        public async Task QueryAllPagingListAsyncTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllPagingListAsync(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        // 分页查询 m --> vm
        [Fact]
        public async Task QueryAllPagingListAsyncVMTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllPagingListAsync<AgentVM>(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
