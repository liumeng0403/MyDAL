using System.Linq;
using System.Data;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Xunit;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Reader;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;

namespace EasyDAL.Exchange.Tests
{
    public class QueryTest : TestBase
    {

        

        // 查询一个已存在对象 单条件
        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest()
        {
            var xx0 = "";

            //  == Guid
            var res = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryFirstOrDefaultAsync();

            var xx1 = "";

            // == DateTime
            var res2 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58.981016"))
                .QueryFirstOrDefaultAsync();

            var xx2 = "";

            // == string
            var res3 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryFirstOrDefaultAsync();

            var xx3 = "";

            // like string
            var res4 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty.Contains("xx"))
                .QueryFirstOrDefaultAsync();

            var xx4 = "";

            // >= obj.DateTime
            var res5 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= testH.CreatedOn)
                .QueryFirstOrDefaultAsync();

            var xx5 = "";

            var start = DateTime.Now.AddDays(-10);
            // >= variable(DateTime)
            var res6 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .QueryFirstOrDefaultAsync();

            var xx6 = "";

            // <= DateTime
            var res7 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .QueryFirstOrDefaultAsync();



            var xx = "";
        }

        // 查询一个已存在对象 多条件
        [Fact]
        public async Task QueryFirstOrDefaultAsyncTest2()
        {
            var xx0 = "";

            // where and
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testH.StartTime)
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync();

            var xx = "";
        }

        // 查询多个已存在对象 单条件
        [Fact]
        public async Task QueryListAsyncTest()
        {
            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-10),
                StartTime = DateTime.Now.AddDays(-10),
                EndTime = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };

            var xx0 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testQ.StartTime)
                .QueryListAsync();

            var xx1 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == testQ.AgentLevelXX)
                .QueryListAsync();

            var xx = "";
        }

    }
}
