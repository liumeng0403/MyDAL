using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using EasyDAL.Exchange.Tests.Enums;
using EasyDAL.Exchange.Tests.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class QueryTest : TestBase
    {   
        private async Task<BodyFitRecord> PreQuery()
        {


            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58"),
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "xxxx"
            };

            // 清理数据
            var resd = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            // 造数据
            var resc = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            return m;
        }

        // 查询一个已存在对象 单条件
        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest()
        {
            var m = await PreQuery();

            var xx0 = "";

            //  == Guid
            var res = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryFirstOrDefaultAsync();

            var tuple0 = (XDebug.SQL, XDebug.Parameters);

            var resR = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .QueryFirstOrDefaultAsync();

            var tupleR0 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res.Id == resR.Id);

            var xx1 = "";

            // == DateTime
            var res2 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .QueryFirstOrDefaultAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var resR2 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .QueryFirstOrDefaultAsync();

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res2.Id == resR2.Id);

            var xx2 = "";

            // == string
            var res3 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryFirstOrDefaultAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var resR3 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .QueryFirstOrDefaultAsync();

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res3.Id == resR3.Id);

            var xx4 = "";

            // >= obj.DateTime
            var res5 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync();

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            var resR5 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryListAsync();

            var tupleR5 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res5.Count == resR5.Count);

            var xx5 = "";

            var start = DateTime.Now.AddDays(-10);
            // >= variable(DateTime)
            var res6 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .QueryListAsync();

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            var resR6 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .QueryListAsync();

            var tupleR6 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res6.Count == resR6.Count);

            var xx6 = "";

            // <= DateTime
            var res7 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .QueryListAsync();

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            var resR7 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .QueryListAsync();

            var tupleR7 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res7.Count == resR7.Count);

            var xx = "";


        }

        // 查询一个已存在对象 多条件
        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest2()
        {
            var xx1 = "";

            // where and
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            var xx = "";
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsyncVMTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync<AgentVM>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        // 查询多个已存在对象 单条件
        [Fact]
        public async Task QueryListAsyncTest()
        {
            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                DateTime_大于等于 = DateTime.Now.AddDays(-30),
                DateTime_小于等于 = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };

            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testQ.DateTime_大于等于)
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == testQ.AgentLevelXX)
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        [Fact]
        public async Task QueryListAsyncVMTest()
        {
            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                DateTime_大于等于 = DateTime.Now.AddDays(-30),
                DateTime_小于等于 = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };

            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testQ.DateTime_大于等于)
                .QueryListAsync<AgentVM>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);
            
            var xx = "";
        }

        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();

            var tuple = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        // 查询 所有
        [Fact]
        public async Task QueryAllAsyncTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

        [Fact]
        public async Task QueryAllAsyncVMTest()
        {
            var xx1 = "";

            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .QueryAllAsync<AgentVM>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
