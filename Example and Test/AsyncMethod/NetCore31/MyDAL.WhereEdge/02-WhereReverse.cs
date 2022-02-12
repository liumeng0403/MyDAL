using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.WhereEdge
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
            var res1 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SelectListAsync();

            

            var resR1 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .SelectListAsync();

            Assert.True(res1.Count == resR1.Count);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            var start = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30).AddDays(-10);

            // >= variable(DateTime)
            var res2 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .SelectListAsync();

            

            var resR2 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .SelectListAsync();

            Assert.True(res2.Count == resR2.Count);

            

            /********************************************************************************************************************************/

            var xx3 = string.Empty;

            // <= DateTime
            var res3 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .SelectListAsync();

            

            var resR3 = await MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .SelectListAsync();

            Assert.True(res3.Count == resR3.Count);

            

            /********************************************************************************************************************************/

        }
    }
}
