using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Tests.Entities;
using EasyDAL.Exchange.Tests.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class JoinWhereTest:TestBase
    {

        [Fact]
        public async Task WhereTest()
        {
            var xx1 = "";

            // const guid
            var res1 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent, out var record)
                .From(() => agent)
                .InnerJoin(() => record)
                .On(() => agent.Id == record.AgentId)
                .Where(() => record.AgentId == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .QueryListAsync<Agent>();
            
            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            // const string
            var res2 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2)
                .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name == "辛文丽")
                .QueryListAsync<AgentInventoryRecord>();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx3 = "";

            // method datetime
            var res3 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                .InnerJoin(() => record3)
                .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.CreatedOn >= DateTime.Now.AddDays(-60))
                .QueryListAsync<AgentInventoryRecord>();

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var xx4 = "";

            // const datetime
            var res4 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent4, out var record4)
                .From(() => agent4)
                .InnerJoin(() => record4)
                .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.CreatedOn >= Convert.ToDateTime("2018-08-16 19:20:28.118853"))
                .QueryListAsync<AgentInventoryRecord>();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            var xx5 = "";

            // const enum
            var res5 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent5, out var record5)
                .From(() => agent5)
                .InnerJoin(() => record5)
                .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel==AgentLevel.DistiAgent)
                .QueryListAsync<AgentInventoryRecord>();

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }

    }
}
