using MyDAL.Test.Entities;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _02_ListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************************************/

            var xx1 = string.Empty;

            // >= obj.DateTime
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .ListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .ListAsync();
            Assert.True(res1.Count == resR1.Count);
            //Assert.True(res1.Count >0);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx2 = string.Empty;

            var start = WhereTest.CreatedOn.AddDays(-10);
            // >= variable(DateTime)
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .ListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .ListAsync();
            Assert.True(res2.Count == resR2.Count);
            //Assert.True(res2.Count > 0);

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx3 = string.Empty;

            // <= DateTime
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .ListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .ListAsync();
            Assert.True(res3.Count == resR3.Count);
            //Assert.True(res3.Count >0 );

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

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
                .ListAsync();
            Assert.True(res4.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx5 = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == testQ.AgentLevelXX)
                .ListAsync();
            Assert.True(res5.Count == 555);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx = string.Empty;

        }
    }
}
