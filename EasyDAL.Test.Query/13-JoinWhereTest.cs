using EasyDAL.Exchange;
using EasyDAL.Test.Entities.EasyDal_Exchange;
using EasyDAL.Test.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Test.Query
{
    public class _13_JoinWhereTest : TestBase
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

            /**************************************************************************************************************************/

            var xx1 = "";

            //
            var res1 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent1, out var record1)
                .From(() => agent1)
                .InnerJoin(() => record1).On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.CreatedOn >= DateTime.Now.AddDays(-60))                                                               //  const  method  DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res1.Count == 574);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx2 = "";

            // 
            var res2 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                .InnerJoin(() => record2).On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.CreatedOn >= Convert.ToDateTime("2018-08-16 19:20:28.118853"))                      //  const  method  DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res2.Count == 523);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx3 = "";

            // 
            var res3 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                .InnerJoin(() => record3).On(() => agent3.Id == record3.AgentId)
                .Where(() => record3.AgentId == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))                  //  const  method  Guid  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res3.Count == 1);
            Assert.Equal(res3.First().Id, Guid.Parse("02dbc81c-5c9a-4cdf-8bf0-016551f756c4"));

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx4 = "";

            // 
            var res4 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent4, out var record4)
                .From(() => agent4)
                .InnerJoin(() => record4).On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name == "辛文丽")                                                                                                            //  const  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res4.Count == 1);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx5 = "";

            // 
            var res5 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent5, out var record5)
                .From(() => agent5)
                .InnerJoin(() => record5).On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)                                                                           //  const  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res5.Count == 574);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx6 = "";

            // 
            var res6 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent6, out var record6)
                .From(() => agent6)
                .InnerJoin(() => record6).On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.Id == m.Id)                                                                                                                              //  virable  prop  Guid  ==
                .QueryListAsync<Agent>();
            Assert.True(res6.Count == 1);
            Assert.Equal(res6.First().Id, Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"));

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx7 = "";

            // 
            var res7 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent7, out var record7)
                .From(() => agent7)
                .InnerJoin(() => record7).On(() => agent7.Id == record7.AgentId)
                .Where(() => agent7.Name == name)                                                                                                                 //  virable  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res7.Count == 1);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx8 = "";

            // 
            var res8 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent8, out var record8)
                .From(() => agent8)
                .InnerJoin(() => record8).On(() => agent8.Id == record8.AgentId)
                .Where(() => agent8.AgentLevel == (AgentLevel)level)                                                                                        //  virable  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res8.Count == 574);

            var tuple8 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx9 = "";

            // 
            var res9 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                .InnerJoin(() => record9).On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.CreatedOn >= WhereTest.CreatedOn)                                                              //  prop prop DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res9.Count == 574);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx = "";

        }

    }
}
