using EasyDAL.Exchange.Core;
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
        public async Task TwoJoinTest()
        {

            var xxx = "";

            var resx = await Conn.OpenDebug()
                .Joiner<Agent,AgentInventoryRecord>(out var agent,out var record)
                .From(()=>agent).InnerJoin(()=>record).On(() => agent.Id == record.AgentId)
                .Where(() => agent.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .And(() => record.CreatedOn >= DateTime.Now.AddDays(-30))
                .QueryListAsync<Agent>();

            var tuple = (XDebug.SQL, XDebug.Parameters);

            var xxxx = "";
        }

    }
}
