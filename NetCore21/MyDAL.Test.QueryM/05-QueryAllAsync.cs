using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _05_QueryAllAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /********************************************************************************************************/

            var xx1 = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryAllAsync();
            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx=string.Empty;

        }
    }
}
