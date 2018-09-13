using Yunyong.DataExchange;
﻿using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class JoinWhereTest:TestBase
    {
        private async Task<Agent> PreData01()
        {
            return await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .QueryFirstOrDefaultAsync();
        }

        [Fact]
        public async Task WhereXTest01()
        {
            var m = await PreData01();
            var name = "辛文丽";
            var level = 128;

            var xx1 = "";

            // method datetime
            var res1 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent, out var record)
                .From(() => agent)
                    .InnerJoin(() => record).On(() => agent.Id == record.AgentId)
                //.Where(() => agent.CreatedOn >= DateTime.Now.AddDays(-60))                                                               //  const  method  DateTime  >=
                //.Where(() => agent.CreatedOn >= Convert.ToDateTime("2018-08-16 19:20:28.118853"))                      //  const  method  DateTime  >=
                //.Where(() => record.AgentId == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))                  //  const  method  Guid  ==
                //.Where(() => agent.Name == "辛文丽")                                                                                                            //  const  string  ==
                //.Where(() => agent.AgentLevel == AgentLevel.DistiAgent)                                                                           //  const  enum  ==
                //.Where(()=>agent.Id==m.Id)                                                                                                                              //  virable  prop  Guid  ==
                //.Where(() => agent.Name == name)                                                                                                                 //  virable  string  ==
                //.Where(()=>agent.AgentLevel==(AgentLevel)level)                                                                                        //  virable  enum  ==
                .Where(() => agent.CreatedOn >= WhereTest.DateTime_大于等于)                                                              //  prop prop DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);
            
            var xx = "";

        }

    }
}
