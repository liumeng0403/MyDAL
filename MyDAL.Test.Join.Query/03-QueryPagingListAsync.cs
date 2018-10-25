using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryM
{
    public class _03_QueryPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent5, out var record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync<Agent>(1, 10);
            Assert.True(res5.TotalCount == 574);

            /*************************************************************************************************************************/

            var xx = "";

        }
    }
}
