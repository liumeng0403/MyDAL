using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryM
{
    public class _02_QueryListAsync : TestBase
    {
        private async Task<Agent> PreData01()
        {
            return await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .QueryOneAsync();
        }

        [Fact]
        public async Task Test01()
        {

            var m = await PreData01();
            var name = "辛文丽";
            var level = 128;

            /**************************************************************************************************************************/

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res2 = await Conn
                .Queryer(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.CreatedOn >= Convert.ToDateTime("2018-08-16 19:20:28.118853"))                      //  const  method  DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res2.Count == 523);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => record3.AgentId == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))                  //  const  method  Guid  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res3.Count == 1);
            Assert.Equal(res3.First().Id, Guid.Parse("02dbc81c-5c9a-4cdf-8bf0-016551f756c4"));

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res4 = await Conn
                .Queryer(out Agent agent4, out AgentInventoryRecord record4)
                .From(() => agent4)
                    .InnerJoin(() => record4)
                        .On(() => agent4.Id == record4.AgentId)
                .Where(() => agent4.Name == "辛文丽")                                                                                                            //  const  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res4.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res5 = await Conn
                .Queryer(out Agent agent5, out AgentInventoryRecord record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)                                                                           //  const  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res5.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res6 = await Conn
                .Queryer(out Agent agent6, out AgentInventoryRecord record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.Id == m.Id)                                                                                                                              //  virable  prop  Guid  ==
                .QueryListAsync<Agent>();
            Assert.True(res6.Count == 1);
            Assert.Equal(res6.First().Id, Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"));

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res7 = await Conn
                .Queryer(out Agent agent7, out AgentInventoryRecord record7)
                .From(() => agent7)
                    .InnerJoin(() => record7)
                        .On(() => agent7.Id == record7.AgentId)
                .Where(() => agent7.Name == name)                                                                                                                 //  virable  string  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res7.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res8 = await Conn
                .Queryer(out Agent agent8, out AgentInventoryRecord record8)
                .From(() => agent8)
                    .InnerJoin(() => record8)
                        .On(() => agent8.Id == record8.AgentId)
                .Where(() => agent8.AgentLevel == (AgentLevel)level)                                                                                        //  virable  enum  ==
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res8.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            // 
            var res9 = await Conn
                .Queryer(out Agent agent9, out AgentInventoryRecord record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))                     //  prop prop DateTime  >=
                .QueryListAsync<AgentInventoryRecord>();
            Assert.True(res9.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

            var res10 = await Conn
                .Queryer(out Agent agent10, out AgentInventoryRecord record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(() => agent10.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                    .And(() => record10.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30).AddDays(-60))
                .QueryListAsync<Agent>();
            Assert.True(res10.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************/

            xx = string.Empty;

        }


        [Fact]
        public async Task Test02()
        {

            /*********************************************************************************************************************************************************/

            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out AspnetUsers user1, out AspnetUserRoles userRole1, out AspnetRoles role1)
                .From(() => user1)
                    .InnerJoin(() => userRole1)
                        .On(() => user1.Id == userRole1.UserId)
                    .InnerJoin(() => role1)
                        .On(() => userRole1.RoleId == role1.Id)
                .OrderBy(() => user1.UserName)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res1.Count == 29180);
            Assert.True(res1.First().UserName == "45285586990");

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            xx = string.Empty;

            // order by id  -- 手动查看
            var res2 = await Conn
                .Queryer(out AspnetUsers user2, out AspnetUserRoles userRole2, out AspnetRoles role2)
                .From(() => user2)
                    .InnerJoin(() => userRole2)
                        .On(() => user2.Id == userRole2.UserId)
                    .InnerJoin(() => role2)
                        .On(() => userRole2.RoleId == role2.Id)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res2.Count == 29180);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            xx=string.Empty;

            // order by createdon -- 手动查看
            var res3 = await Conn
                .Queryer(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer(out AspnetUsers user4, out AspnetUserRoles userRole4, out AspnetRoles role4)
                .From(() => user4)
                    .InnerJoin(() => userRole4)
                        .On(() => user4.Id == userRole4.UserId)
                    .InnerJoin(() => role4)
                        .On(() => userRole4.RoleId == role4.Id)
                .Where(() => user4.NickName.StartsWith("刘"))
                .OrderBy(() => user4.UserName)
                    .ThenOrderBy(() => user4.AgentLevel, OrderByEnum.Asc)
                .QueryListAsync<AspnetUsers>();
            Assert.True(res4.Count == 1480);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
