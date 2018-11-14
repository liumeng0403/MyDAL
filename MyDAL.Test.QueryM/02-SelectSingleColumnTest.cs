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

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Selecter<Agent>()
                .Distinct()
                .QueryAllAsync(it => it.Name);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx = "";

        }

    }
}
