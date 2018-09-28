using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Query
{
    public class _12_WhereTest:TestBase
    {

        [Fact]
        public async Task WhereTestx()
        {

            /************************************************************************************************************************/

            var xx1 = "";

            // is null 
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .QueryListAsync();
            Assert.True(res1.Count == 28066);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx2 = "";

            // is not null 
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .QueryListAsync();
            Assert.True(res1.Count == 554);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /************************************************************************************************************************/

            var xx = "";

        }

    }
}
