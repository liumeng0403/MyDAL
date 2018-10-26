using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryM
{
    public class _05_QueryAllAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .QueryAllAsync();
            Assert.True(res1.Count == 28620);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************/

            var xx = "";

        }
    }
}
