using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _04_QueryAllPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************************************************************/

            var xx7 = "";

            var res7 = await Conn
                .Selecter<Agent>()
                .QueryAllPagingListAsync<AgentVM>(1, 10);
            Assert.True(res7.TotalCount == 28620);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx = "";

        }
    }
}
