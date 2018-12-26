using MyDAL.Test.Entities.MyDAL_TestDB;
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
    public class _03_PagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = "";

            var res9 = await Conn
                .Queryer(out Agent agent9, out AgentInventoryRecord record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent9.Name,
                    YYYY = agent9.PathId
                });
            Assert.True(res9.TotalCount == 574);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            xx = "";

            var option10 = new AgentQueryOption();
            option10.AgentLevel = AgentLevel.DistiAgent;
            var res10 = await Conn
                .Queryer(out Agent agent10, out AgentInventoryRecord record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync(option10, () => new AgentVM
                {
                    XXXX = agent10.Name,
                    YYYY = agent10.PathId
                });
            Assert.True(res10.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = "";

            var option11 = new AgentQueryOption();
            option11.AgentLevel = AgentLevel.DistiAgent;
            option11.PageIndex = 5;
            option11.PageSize = 10;
            var res11 = await Conn
                .Queryer(out Agent agent11, out AgentInventoryRecord record11)
                .From(() => agent11)
                    .InnerJoin(() => record11)
                        .On(() => agent11.Id == record11.AgentId)
                .Where(() => agent11.AgentLevel == AgentLevel.DistiAgent)
                .PagingListAsync(option11, () => new AgentVM
                {
                    XXXX = agent11.Name,
                    YYYY = agent11.PathId
                });
            Assert.True(res11.TotalCount == 574);
            Assert.True(res11.PageSize == 10);
            Assert.True(res11.PageIndex == 5);
            Assert.True(res11.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }
    }
}
