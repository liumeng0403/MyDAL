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

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var xx = "";
        }
    }
}
