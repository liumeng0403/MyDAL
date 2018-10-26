using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

            var xx = "";

        }

    }
}
