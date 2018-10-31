using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.WhereEdge
{
    public class _09_WhereMultiCondition:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var guid1 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var pathId1 = "~00-d-3-2-1-c-2-f-4-3-1-2-4";
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == guid1 && it.PathId == pathId1)
                .QueryListAsync();
            Assert.True(res1.Count == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx2 = "";

            var guid2 = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            var pathId2 = "~00-d-3-1-1-5-1-3-4-2-2-1-1-11-6-1-8-4";
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == guid2 || it.PathId == pathId2)
                .QueryListAsync();
            Assert.True(res2.Count == 2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx = "";
        }
    }
}
