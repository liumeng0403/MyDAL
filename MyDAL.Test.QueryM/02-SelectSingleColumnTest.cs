using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Query
{
    public class _02_SelectSingleColumnTest:TestBase
    {

        [Fact]
        public async Task test()
        {

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .QueryAllAsync(it => it.Id);
            Assert.True(res1.Count == 28620);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .Distinct()
                .QueryAllAsync(it => it.Name);
            Assert.True(res2.Count == 24444);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .Distinct()
                .QueryAllAsync(it => it.CreatedOn.ToString("yyyy-MM-dd"));
            Assert.True(res3.Count == 2);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx = "";

        }

    }
}
