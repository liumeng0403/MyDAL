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
    public class _03_PagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer(out Agent agent5, out AgentInventoryRecord record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync<Agent>(1, 10);
            Assert.True(res5.TotalCount == 574);

            /*************************************************************************************************************************/

            var option6 = new AgentQueryOption();
            option6.AgentLevel = AgentLevel.DistiAgent;

            xx = string.Empty;

            var res6 = await Conn
                .Queryer(out Agent agent6, out AgentInventoryRecord record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync<Agent>(option6);
            Assert.True(res6.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
