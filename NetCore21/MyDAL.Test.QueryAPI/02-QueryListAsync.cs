using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _02_QueryListAsync
        : TestBase
    {

        private async Task<Agent> PreData01()
        {
            return await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("0ce552c0-2f5e-4c22-b26d-01654443b30e"))
                .QueryOneAsync();
        }

        [Fact]
        public async Task History_06()
        {

            var m = await PreData01();
            var name = "辛文丽";
            var level = 128;

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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

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
        public async Task History_07()
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

        [Fact]
        public async Task History_08()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .QueryListAsync<Agent>();

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_QueryAllData_QuerySingleColumn_Shortcut()
        {
            var xx3 = string.Empty;

            var res3 = await Conn.QueryListAsync<Agent, Guid>(null, it => it.Id);

            Assert.True(res3.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/


        }

        [Fact]
        public async Task Mock_QueryAllData_QueryM_Shortcut()
        {
            xx = string.Empty;

            var res1 = await Conn.QueryListAsync<Agent>(null);

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/


        }

        [Fact]
        public async Task Mock_QueryAllData_QueryVM_Shortcut()
        {
            xx = string.Empty;

            var res2 = await Conn.QueryListAsync<Agent, AgentVM>(null);

            Assert.True(res2.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/


        }

        [Fact]
        public async Task Mock_QueryAllData_QueryVMColumn_Shortcut()
        {
            var xx4 = string.Empty;

            var res4 = await Conn.QueryListAsync<Agent, AgentVM>(null, it => new AgentVM
            {
                XXXX = it.Name,
                YYYY = it.PathId
            });

            Assert.True(res4.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_Shortcut()
        {

            xx = string.Empty;

            var res7 = await Conn.QueryListAsync<Agent, string>(it => it.Name.StartsWith("张"), it => it.Name);

            Assert.True(res7.Count == 1996);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryM_Shortcut()
        {

            xx = string.Empty;

            var res2 = await Conn.QueryListAsync<AlipayPaymentRecord>(it => it.CreatedOn >= DateTime.Parse("2018-08-20"));

            Assert.True(res2.Count == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_Shortcut()
        {

            xx = string.Empty;

            var res2 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= DateTime.Parse("2018-08-20"));

            Assert.True(res2.Count == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

        }

        [Fact]
        public async Task QueryVMColumn_Shortcut()
        {

            xx = string.Empty;

            var res3 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= DateTime.Parse("2018-08-20"),
            it => new AlipayPaymentRecordVM
            {
                TotalAmount = it.TotalAmount,
                Description = it.Description
            });

            Assert.True(res3.Count == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        [Fact]
        public async Task Mock_QueryAllData_QuerySingleColumn_ST()
        {

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryListAsync(it => it.Id);

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_QueryAllData_QueryM_ST()
        {

            /********************************************************************************************************/

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryListAsync();

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_QueryAllData_QueryVM_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryListAsync<AgentVM>();

            Assert.True(res1.Count == 28620);
            Assert.NotNull(res1.First().Name);
            Assert.Null(res1.First().XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_QueryAllData_QueryVMColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryListAsync(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(it => it.Name);

            Assert.True(res1.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_ST()
        {

            xx = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryListAsync();

            Assert.True(res4.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_ST()
        {
            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryListAsync<AgentVM>();

            Assert.True(res5.Count == 28619);
            Assert.NotNull(res5.First().Name);
            Assert.Null(res5.First().XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVMColumn_ST()
        {

            xx = string.Empty;

            /*************************************************************************************************************************/

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res5.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_QueryAllData_QuerySingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .QueryListAsync(() => agent1.Id);

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_QueryAllData_QueryM_MT()
        {

            xx = string.Empty;

            // 
            var res1 = await Conn
                .Queryer(out AspnetUsers user, out AspnetUserRoles userRole, out AspnetRoles role)
                .From(() => user)
                    .InnerJoin(() => userRole)
                        .On(() => user.Id == userRole.UserId)
                    .InnerJoin(() => role)
                        .On(() => userRole.RoleId == role.Id)
                .QueryListAsync<AspnetUsers>();

            Assert.True(res1.Count == 29180);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*********************************************************************************************************************************************************/

        }

        [Fact]
        public async Task Mock_QueryAllData_QueryVMColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .QueryListAsync(()=>new AgentVM
                {
                    XXXX=agent.Name,
                    YYYY=agent.PathId
                });

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync(() => agent1.CreatedOn);

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_MT()
        {

            xx = string.Empty;

            //
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

        }

        [Fact]
        public async Task QueryVMColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => record.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryListAsync(() => new AgentVM
                {
                    nn = agent.PathId,
                    yy = record.Id,
                    xx = agent.Id,
                    zz = agent.Name,
                    mm = record.LockedCount
                });

            Assert.True(res1.Count == 574);
            Assert.True("~00-d-3-2-1-c-2-1a-1" == res1.First().nn);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);
            
            /*************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_SQL()
        {
            xx = string.Empty;

            var sql = @"
                                    select agent1.`CreatedOn`
                                    from `agent` as agent1 
	                                    inner join `agentinventoryrecord` as record1
		                                    on agent1.`Id`=record1.`AgentId`
                                    where  agent1.`AgentLevel`=@AgentLevel;
                                ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.MySQL_Int}
            };

            var res1 = await Conn.QueryListAsync<DateTime>(sql, paras);

            Assert.True(res1.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_SQL()
        {
            xx = string.Empty;

            var sql = @"
                                    select 	agent12.`PathId` as nn,
	                                    record12.`Id` as yy,
	                                    agent12.`Id` as xx,
	                                    agent12.`Name` as zz,
	                                    record12.`LockedCount` as mm
                                    from `agent` as agent12 
	                                    inner join `agentinventoryrecord` as record12
		                                    on agent12.`Id`=record12.`AgentId`
                                    where  record12.`CreatedOn`>=@CreatedOn;
                                ";

            var param = new List<XParam>
            {
                new XParam{ParamName="@CreatedOn",ParamValue=Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),ParamType= ParamTypeEnum.MySQL_DateTime}
            };

            var res1 = await Conn.QueryListAsync<AgentVM>(sql, param);

            Assert.True(res1.Count == 574);
            Assert.True("~00-d-3-2-1-c-2-1a-1" == res1.First().nn);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

    }
}
