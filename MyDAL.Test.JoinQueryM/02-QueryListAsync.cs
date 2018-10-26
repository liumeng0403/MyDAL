using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryM
{
    public class _02_QueryListAsync:TestBase
    {
        private async Task<Agent> PreData01()
        {
            return await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .QueryFirstOrDefaultAsync();
        }

        [Fact]
        public async Task test()
        {

            var m = await PreData01();
            var name = "辛文丽";
            var level = 128;

            /**************************************************************************************************************************/

            var xx1 = "";

            //
            var res1 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent1, out var record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.CreatedOn >= WhereTest.CreatedOn.AddDays(-60))                                                               //  const  method  DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res1.Count == 574);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx2 = "";

            // 
            var res2 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent2, out var record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.CreatedOn >= Convert.ToDateTime("2018-08-16 19:20:28.118853"))                      //  const  method  DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res2.Count == 523);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx3 = "";

            // 
            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => record3.AgentId == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))                  //  const  method  Guid  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res3.Count == 1);
            Assert.Equal(res3.First().Id, Guid.Parse("02dbc81c-5c9a-4cdf-8bf0-016551f756c4"));

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx4 = "";

            // 
            var res4 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent4, out var record4)
                .From(() => agent4)
                    .InnerJoin(() => record4)
                        .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name == "辛文丽")                                                                                                            //  const  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res4.Count == 1);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx5 = "";

            // 
            var res5 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent5, out var record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)                                                                           //  const  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res5.Count == 574);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx6 = "";

            // 
            var res6 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent6, out var record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.Id == m.Id)                                                                                                                              //  virable  prop  Guid  ==
                .QueryListAsync<Agent>();
            Assert.True(res6.Count == 1);
            Assert.Equal(res6.First().Id, Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"));

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx7 = "";

            // 
            var res7 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent7, out var record7)
                .From(() => agent7)
                    .InnerJoin(() => record7)
                        .On(() => agent7.Id == record7.AgentId)
                .Where(() => agent7.Name == name)                                                                                                                 //  virable  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res7.Count == 1);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx8 = "";

            // 
            var res8 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent8, out var record8)
                .From(() => agent8)
                    .InnerJoin(() => record8)
                        .On(() => agent8.Id == record8.AgentId)
                .Where(() => agent8.AgentLevel == (AgentLevel)level)                                                                                        //  virable  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res8.Count == 574);

            var tuple8 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx9 = "";

            // 
            var res9 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent9, out var record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.CreatedOn >= WhereTest.CreatedOn)                                                              //  prop prop DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res9.Count == 574);

            var tuple9 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/
            
            var xx10 = "";

            var res10 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent10, out var record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                    .And(() => record10.CreatedOn >= WhereTest.CreatedOn.AddDays(-60))
                .QueryListAsync<Agent>();
            Assert.True(res10.Count == 1);

            var tuple10 = (XDebug.SQL, XDebug.Parameters);

            /**************************************************************************************************************************/

            var xx = "";

        }


        [Fact]
        public async Task test2()
        {

            /*********************************************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user1, out var userRole1, out var role1)
                .From(() => user1)
                    .InnerJoin(() => userRole1)
                        .On(() => user1.Id == userRole1.UserId)
                    .InnerJoin(() => role1)
                        .On(() => userRole1.RoleId == role1.Id)
                .OrderBy(() => user1.UserName)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res1.Count == 29180);
            Assert.True(res1.First().UserName == "45285586990");

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx2 = "";

            // order by id  -- 手动查看
            var res2 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user2, out var userRole2, out var role2)
                .From(() => user2)
                    .InnerJoin(() => userRole2)
                        .On(() => user2.Id == userRole2.UserId)
                    .InnerJoin(() => role2)
                        .On(() => userRole2.RoleId == role2.Id)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res2.Count == 29180);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx3 = "";

            // order by createdon -- 手动查看
            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 574);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Joiner<AspnetUsers, AspnetUserRoles, AspnetRoles>(out var user4, out var userRole4, out var role4)
                .From(() => user4)
                    .InnerJoin(() => userRole4)
                        .On(() => user4.Id == userRole4.UserId)
                    .InnerJoin(() => role4)
                        .On(() => userRole4.RoleId == role4.Id)
                .OrderBy(() => user4.UserName)
                    .ThenOrderBy(() => user4.AgentLevel, OrderByEnum.Asc)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res4.Count == 29180);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            var xx = "";

        }

    }
}
