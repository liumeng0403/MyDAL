using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _02_WhereReverse
        :TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************************************/

            xx = string.Empty;

            // >= obj.DateTime
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .QueryListAsync();

            Assert.True(res1.Count == resR1.Count);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

            var start = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30).AddDays(-10);

            // >= variable(DateTime)
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .QueryListAsync();

            Assert.True(res2.Count == resR2.Count);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            var xx3 = string.Empty;

            // <= DateTime
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .QueryListAsync();

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .QueryListAsync();

            Assert.True(res3.Count == resR3.Count);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

        }
    }
}
