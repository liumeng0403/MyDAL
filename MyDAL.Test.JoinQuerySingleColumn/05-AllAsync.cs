using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQuerySingleColumn
{
    public class _05_AllAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .AllAsync(() => agent1.Id);
            Assert.True(res1.Count == 574);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
