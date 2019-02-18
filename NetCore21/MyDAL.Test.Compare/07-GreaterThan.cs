using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _07_GreaterThan
        : TestBase
    {

        [Fact]
        public async Task GreaterThan()
        {
            xx = string.Empty;

            // > --> >
            var res1 = await Conn.QueryListAsync<Agent>(it => it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30));

            Assert.True(res1.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task NotGreaterThan()
        {
            xx = string.Empty;

            // !(>) --> <=
            var res1 = await Conn.QueryListAsync<Agent>(it => !(it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30)));

            Assert.True(res1.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************************************************************/

            xx = string.Empty;

            // <= --> <=
            var res2 = await Conn.QueryListAsync<Agent>(it => it.CreatedOn <= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30));

            Assert.True(res2.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
