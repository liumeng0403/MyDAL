using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _05_AllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .AllAsync(it => it.Id);
            Assert.True(res1.Count == 28620);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.Name);
            Assert.True(res2.Count == 24444);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy-MM-dd"));
            Assert.True(res3.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy-MM"));
            Assert.True(res4.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Distinct()
                .AllAsync(it => it.CreatedOn.ToString("yyyy"));
            Assert.True(res5.Count == 2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
