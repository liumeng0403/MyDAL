using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _04_PagingAllAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************************************************************/

            var xx = string.Empty;

            var res7 = await Conn
                .Queryer<Agent>()
                .PagingAllAsync<AgentVM>(1, 10);
            Assert.True(res7.TotalCount == 28620);

            var tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
