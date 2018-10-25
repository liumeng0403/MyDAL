using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryVmColumn
{
    public class _03_QueryPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx9 = "";

            var res9 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent9.Name,
                    YYYY = agent9.PathId
                });
            Assert.True(res9.TotalCount == 574);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx10 = "";

            var option10 = new AgentQueryOption();
            option10.AgentLevel = AgentLevel.DistiAgent;
            var res10 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent10, out var record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(option10, () => new AgentVM
                {
                    XXXX = agent10.Name,
                    YYYY = agent10.PathId
                });
            Assert.True(res10.TotalCount == 574);

            var tuple10 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx11 = "";

            var option11 = new AgentQueryOption();
            option11.AgentLevel = AgentLevel.DistiAgent;
            option11.PageIndex = 5;
            option11.PageSize = 10;
            var res11 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent11, out var record11)
                .From(() => agent11)
                    .InnerJoin(() => record11)
                        .On(() => agent11.Id == record11.AgentId)
                .Where(() => agent11.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingListAsync(option11, () => new AgentVM
                {
                    XXXX = agent11.Name,
                    YYYY = agent11.PathId
                });
            Assert.True(res11.TotalCount == 574);
            Assert.True(res11.PageSize == 10);
            Assert.True(res11.PageIndex == 5);
            Assert.True(res11.Data.Count == 10);

            var tuple11 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

        }
    }
}
