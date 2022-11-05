using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _07_GreaterThan
        : TestBase
    {

        [Fact]
        public void GreaterThan()
        {
            xx = string.Empty;

            // > --> >
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30));

            Assert.True(res1.Count == 28619);

            

            xx = string.Empty;
        }

        [Fact]
        public void NotGreaterThan()
        {
            xx = string.Empty;

            // !(>) --> <=
            var res1 = MyDAL_TestDB.SelectList<Agent>(it => !(it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30)));

            Assert.True(res1.Count == 1);

            

            /******************************************************************************************************************************************************/

            xx = string.Empty;

            // <= --> <=
            var res2 = MyDAL_TestDB.SelectList<Agent>(it => it.CreatedOn <= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30));

            Assert.True(res2.Count == 1);

            

            /******************************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
