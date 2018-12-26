using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Queryer<Agent>()
                .TopAsync(25);
            Assert.True(res1.Count == 25);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }
    }
}
