using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _01_FirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;
            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .FirstOrDefaultAsync<AgentVM>();
            Assert.NotNull(res1);
            Assert.Null(res1.XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
