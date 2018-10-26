using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
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

            var option6 = new AgentQueryOption();
            option6.AgentLevel = AgentLevel.DistiAgent;

            var xx6 = "";

            var res6 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent6, out var record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync<Agent>(option6);
            Assert.True(res6.TotalCount == 574);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx = "";

        }
    }
}
