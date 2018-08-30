using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class JoinTest : TestBase
    {

        [Fact]
        public async Task XXtest()
        {

            var xxx = "";

            var resx = await Conn.OpenHint()
                .Joiner()
                .From<Agent>(out var agent, "agent")
                .InnerJoin<AgentInventoryRecord>(out var record, "record")
                .On(() => agent.Id == record.AgentId)
                .Where(() => agent.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .And(() => record.CreatedOn >= DateTime.Now.AddDays(-30))
                .QueryListAsync<Agent>();

            var tuple = (Hints.SQL, Hints.Parameters);

            var xxxx = "";
        }

    }
}
