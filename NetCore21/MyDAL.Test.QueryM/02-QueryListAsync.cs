using MyDAL.Test.Entities;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _02_QueryListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************************************/

            xx = string.Empty;

            // >= obj.DateTime
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res1.Count == resR1.Count);
            //Assert.True(res1.Count >0);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

            var start = WhereTest.CreatedOn.AddDays(-10);
            // >= variable(DateTime)
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res2.Count == resR2.Count);
            //Assert.True(res2.Count > 0);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx3 = string.Empty;

            // <= DateTime
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .QueryListAsync();
            Assert.True(res3.Count == resR3.Count);
            //Assert.True(res3.Count >0 );

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var testQ = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                StartTime = WhereTest.CreatedOn,
                EndTime = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };

            var xx4 = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= testQ.StartTime)
                .QueryListAsync();
            Assert.True(res4.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx5 = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == testQ.AgentLevelXX)
                .QueryListAsync();
            Assert.True(res5.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
