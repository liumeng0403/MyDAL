using MyDAL.Test.Entities;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryM
{
    public class _02_QueryListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************************************/

            var xx1 = "";

            // >= obj.DateTime
            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res1.Count == resR1.Count);
            //Assert.True(res1.Count >0);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx2 = "";

            var start = WhereTest.CreatedOn.AddDays(-10);
            // >= variable(DateTime)
            var res2 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var resR2 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res2.Count == resR2.Count);
            //Assert.True(res2.Count > 0);

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx3 = "";

            // <= DateTime
            var res3 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .QueryListAsync();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var resR3 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res3.Count == resR3.Count);
            //Assert.True(res3.Count >0 );

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                DateTime_大于等于 = WhereTest.CreatedOn,
                DateTime_小于等于 = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };

            var xx4 = "";

            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= testQ.DateTime_大于等于)
                .QueryListAsync();
            Assert.True(res4.Count == 28619);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == testQ.AgentLevelXX)
                .QueryListAsync();
            Assert.True(res5.Count == 555);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx = "";

        }
    }
}
