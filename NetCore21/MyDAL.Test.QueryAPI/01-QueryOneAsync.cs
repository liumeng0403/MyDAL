using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _01_QueryOneAsync
        : TestBase
    {
        private async Task<BodyFitRecord> PreQuery()
        {


            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58"),
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "xxxx"
            };

            // 清理数据
            var resd = await Conn.DeleteAsync<BodyFitRecord>(it => it.Id == m.Id);

            // 造数据
            var resc = await Conn.CreateAsync(m);

            return m;
        }

        [Fact]
        public async Task History_01()
        {

            await PreQuery();

            /****************************************************************************************************************************************/

            xx = string.Empty;

            //  == Guid
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryOneAsync();

            Assert.NotNull(res1);

            var resR1 = await Conn.OpenDebug()
                .Queryer<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .QueryOneAsync();

            Assert.NotNull(resR1);
            Assert.True(res1.Id == resR1.Id);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == DateTime
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .QueryOneAsync();

            Assert.NotNull(res2);

            var resR2 = await Conn.OpenDebug()
                .Queryer<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .QueryOneAsync();

            Assert.NotNull(resR2);
            Assert.True(res2.Id == resR2.Id);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == string
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryOneAsync();

            Assert.NotNull(res3);

            var resR3 = await Conn.OpenDebug()
                .Queryer<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .QueryOneAsync();

            Assert.NotNull(resR3);
            Assert.True(res3.Id == resR3.Id);

            /****************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_Shortcut()
        {
            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, Guid>(it => it.Id == pk && it.CreatedOn == date, it => it.Id);

            Assert.True(res1 == pk);

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);

            Assert.NotNull(res1);

            

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVM_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);

            Assert.NotNull(res1);

            

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVMColumn_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
            it => new AlipayPaymentRecordVM
            {
                Id = it.Id,
                TotalAmount = it.TotalAmount,
                Description = it.Description
            });

            Assert.NotNull(res1);

            

            /****************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QuerySingleColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn == DateTime.Parse("2018-08-16 19:22:01.716307"))
                .QueryOneAsync(it => it.Id);

            Assert.True(res1.Equals(Guid.Parse("000f5f16-5502-4324-b5d6-016544300263")));

            

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_ST()
        {

            xx = string.Empty;

            // 
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                    .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync();

            Assert.NotNull(res4);

            

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_ST()
        {

            xx = string.Empty;

            /****************************************************************************************************************************************/

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync<AgentVM>();

            Assert.NotNull(res1);
            Assert.Null(res1.XXXX);

            

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVMColumn_ST()
        {

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.Equal("樊士芹", res1.XXXX);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QuerySingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord agentRecord)
                .From(() => agent)
                    .InnerJoin(() => agentRecord)
                        .On(() => agent.Id == agentRecord.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .QueryOneAsync(() => agent.Name);

            Assert.NotNull(res1);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .QueryOneAsync<Agent>();

            Assert.NotNull(res1);
            Assert.Equal("夏明君", res1.Name);

            

            /****************************************************************************************************************************************/

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
                .Where(() => agent.Id == Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef"))
                .QueryOneAsync(() => new AgentVM
                {
                    nn = agent.PathId,
                    yy = record.Id,
                    xx = agent.Id,
                    zz = agent.Name,
                    mm = record.LockedCount
                });

            Assert.NotNull(res1);
            Assert.Equal("夏明君", res1.zz);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task QuerySingleColumn_SQL()
        {
            xx = string.Empty;

            var sql = @"
                                    select agent.`Name`
                                    from `agent` as agent 
	                                    inner join `agentinventoryrecord` as agentRecord
		                                    on agent.`Id`=agentRecord.`AgentId`
                                    where  agent.`AgentLevel`=@AgentLevel
                                    limit 0,1;
                                ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent}
            };

            var res1 = await Conn.QueryOneAsync<string>(sql, paras);

            Assert.True(res1.Length > 1);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVM_SQL()
        {
            xx = string.Empty;

            var sql = @"
                                    select 	agent.`PathId` as nn,
	                                    record.`Id` as yy,
	                                    agent.`Id` as xx,
	                                    agent.`Name` as zz,
	                                    record.`LockedCount` as mm
                                    from `agent` as agent 
	                                    inner join `agentinventoryrecord` as record
		                                    on agent.`Id`=record.`AgentId`
                                    where  agent.`Id`=@Id
                                    limit 0,1;
                                ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="Id",ParamValue=Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef")}
            };

            var res1 = await Conn.QueryOneAsync<AgentVM>(sql, paras);

            Assert.NotNull(res1);
            Assert.Equal("夏明君", res1.zz);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVM_SQL_NoneParam()
        {
            xx = string.Empty;

            var sql = @"
                                    select 	agent.`PathId` as nn,
	                                    record.`Id` as yy,
	                                    agent.`Id` as xx,
	                                    agent.`Name` as zz,
	                                    record.`LockedCount` as mm
                                    from `agent` as agent 
	                                    inner join `agentinventoryrecord` as record
		                                    on agent.`Id`=record.`AgentId`
                                    where  agent.`Id`='544b9053-322e-4857-89a0-0165443dcbef'
                                    limit 0,1;
                                ";
            
            var res1 = await Conn.QueryOneAsync<AgentVM>(sql);

            Assert.NotNull(res1);
            Assert.Equal("夏明君", res1.zz);

            

            xx = string.Empty;
        }

    }
}
