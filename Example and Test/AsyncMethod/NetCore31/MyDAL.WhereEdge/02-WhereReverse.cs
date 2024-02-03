using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using Xunit;

namespace MyDAL.WhereEdge
{
    public class _02_WhereReverse
        :TestBase
    {
        [Fact]
        public void test()
        {

            /********************************************************************************************************************************/

            xx = string.Empty;

            // >= obj.DateTime
            var res1 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SelectList();

            

            var resR1 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .SelectList();

            Assert.True(res1.Count == resR1.Count);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            var start = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30).AddDays(-10);

            // >= variable(DateTime)
            var res2 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn >= start)
                .SelectList();

            

            var resR2 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => start <= it.CreatedOn)
                .SelectList();

            Assert.True(res2.Count == resR2.Count);

            

            /********************************************************************************************************************************/

            var xx3 = string.Empty;

            // <= DateTime
            var res3 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn <= DateTime.Now)
                .SelectList();

            

            var resR3 = MyDAL_TestDB
                .Selecter<BodyFitRecord>()
                .Where(it => DateTime.Now >= it.CreatedOn)
                .SelectList();

            Assert.True(res3.Count == resR3.Count);

            

            /********************************************************************************************************************************/

        }
    }
}
