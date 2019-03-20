using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _03_QueryPagingAsync
        : TestBase
    {

        [Fact]
        public async Task History_01()
        {
            xx = string.Empty;

            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.StartsWith("张"))
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .QueryPagingAsync(1, 10, it => it.Name);

            Assert.True(res2.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task History_02()
        {

            /******************************************************************************************************/

            xx = string.Empty;

            // order by
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)128)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .QueryPagingAsync(1, 10);

            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************/

            xx = string.Empty;

            // key
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .QueryPagingAsync(1, 10);

            Assert.True(res2.TotalPage == 2807);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************/

            xx = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingAsync(1, 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR4 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingAsync(1, 10);

            Assert.True(res4.TotalCount == resR4.TotalCount);
            Assert.True(res4.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }

        [Fact]
        public async Task History_03()
        {

            xx = string.Empty;

            // where method -- option orderby 
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name == "樊士芹")
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .QueryPagingAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res7.Data.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task History_04()
        {

            xx = string.Empty;

            var option11 = new Join_AgentQueryOption();
            option11.AgentLevel = AgentLevel.DistiAgent;
            option11.PageIndex = 5;
            option11.PageSize = 10;

            var res11 = await Conn
                .Queryer(out Agent agent11, out AgentInventoryRecord record11)
                .From(() => agent11)
                    .InnerJoin(() => record11)
                        .On(() => agent11.Id == record11.AgentId)
                .Where(option11)
                .QueryPagingAsync(() => new AgentVM
                {
                    XXXX = agent11.Name,
                    YYYY = agent11.PathId
                });

            Assert.True(res11.TotalCount == 574);
            Assert.True(res11.PageSize == 10);
            Assert.True(res11.PageIndex == 5);
            Assert.True(res11.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task QuerySingleColumn_Option_ST()
        {
            xx = string.Empty;

            var op1 = new AgentQueryOption();
            op1.AgentLevel = AgentLevel.DistiAgent;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .QueryPagingAsync(it => it.Id);

            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_Option_ST()
        {

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option = new AgentQueryOption();

            option.PageIndex = 1;
            option.PageSize = 10;

            option.StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30);
            option.EndTime = DateTime.Now;
            option.AgentLevel = AgentLevel.DistiAgent;

            // 
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(option)
                .QueryPagingAsync();

            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_Option_ST()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            // 无条件
            var option13 = new AgentQueryOption();

            var res13 = await Conn
                .Queryer<Agent>()
                .Where(option13)
                .QueryPagingAsync<AgentVM>();

            Assert.True(res13.TotalCount == 28620);
            Assert.True(res13.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/


            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVmColumn_Option_ST()
        {
            xx = string.Empty;

            var op1 = new AgentQueryOption();
            op1.AgentLevel = AgentLevel.DistiAgent;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .QueryPagingAsync(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task Mock_QueryAllPaging_QuerySingleColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync(1, 10, it => it.Id);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_QueryAllPaging_QueryM_ST()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync(1, 10);

            Assert.True(res3.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        [Fact]
        public async Task Mock_QueryAllPaging_QueryVM_ST()
        {

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res7 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync<AgentVM>(1, 10);

            Assert.True(res7.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_QueryAllPaging_QueryVmColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .QueryPagingAsync(1, 10, it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task QuerySingleColumn_ST()
        {

            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

        }

        [Fact]
        public async Task QueryM_ST()
        {

            xx = string.Empty;

            // 
            var res1 = await Conn
                .Queryer<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .QueryPagingAsync(1, 10);

            Assert.True(res1.TotalPage == 56);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /******************************************************************************************************/

        }

        [Fact]
        public async Task QueryVM_ST()
        {

            xx = string.Empty;

            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryPagingAsync<AgentVM>(1, 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR6 = await Conn
                .Queryer<Agent>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .QueryPagingAsync<AgentVM>(1, 10);

            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);


            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVmColumn_ST()
        {

            xx = string.Empty;

            var res8 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryPagingAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res8.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task QuerySingleColumn_Option_MT()
        {

            xx = string.Empty;

            var option = new Join_AgentQueryOption();
            option.AgentLevel = AgentLevel.DistiAgent;

            var res10 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(option)
                .QueryPagingAsync(() => agent.Name);

            Assert.True(res10.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryM_Option_MT()
        {
            xx = string.Empty;

            /*************************************************************************************************************************/

            var option6 = new Join_AgentQueryOption();
            option6.AgentLevel = AgentLevel.DistiAgent;

            xx = string.Empty;

            var res6 = await Conn
                .Queryer(out Agent agent6, out AgentInventoryRecord record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(option6)
                .QueryPagingAsync<Agent>();

            Assert.True(res6.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVmColumn_Option_MT()
        {

            xx = string.Empty;

            var option10 = new Join_AgentQueryOption();
            option10.AgentLevel = AgentLevel.DistiAgent;

            var res10 = await Conn
                .Queryer(out Agent agent10, out AgentInventoryRecord record10)
                .From(() => agent10)
                    .InnerJoin(() => record10)
                        .On(() => agent10.Id == record10.AgentId)
                .Where(option10) //
                .QueryPagingAsync(() => new AgentVM
                {
                    XXXX = agent10.Name,
                    YYYY = agent10.PathId
                });

            Assert.True(res10.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task Mock_NoneCondition_QuerySingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .QueryPagingAsync(1, 10, () => agent.Name);

            Assert.True(res1.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_NoneCondition_QueryM_MT()
        {

            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .QueryPagingAsync<Agent>(1, 10);

            Assert.True(res1.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_NoneCondition_QueryVmColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .QueryPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res1.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        /*********************************************************************************************************************************************************/

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
                .QueryPagingAsync(1, 10, () => agent1.Id);

            Assert.True(res1.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryM_MT()
        {

            xx = string.Empty;

            var res5 = await Conn
                .Queryer(out Agent agent5, out AgentInventoryRecord record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync<Agent>(1, 10);

            Assert.True(res5.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }

        [Fact]
        public async Task QueryVmColumn_MT()
        {

            xx = string.Empty;

            var res9 = await Conn
                .Queryer(out Agent agent9, out AgentInventoryRecord record9)
                .From(() => agent9)
                    .InnerJoin(() => record9)
                        .On(() => agent9.Id == record9.AgentId)
                .Where(() => agent9.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent9.Name,
                    YYYY = agent9.PathId
                });

            Assert.True(res9.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task QuerySingleColumn_SQL()
        {

            xx = string.Empty;

            var totalSql = @"
                                            select  count(agent1.`Id`)
                                            from `agent` as agent1 
	                                            inner join `agentinventoryrecord` as record1
		                                            on agent1.`Id`=record1.`AgentId`
                                            where  agent1.`AgentLevel`=@AgentLevel;
                                        ";

            var dataSql = @"
                                            select agent1.`Id`
                                            from `agent` as agent1 
	                                            inner join `agentinventoryrecord` as record1
		                                            on agent1.`Id`=record1.`AgentId`
                                            where  agent1.`AgentLevel`=@AgentLevel
                                            order by agent1.`Id` desc
                                            limit 0,10;
                                        ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.MySQL_Int}
            };

            var res1 = await Conn.QueryPagingAsync<Guid>(1, 10, totalSql, dataSql, paras);

            Assert.True(res1.TotalPage == 58);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_SQL()
        {
            xx = string.Empty;

            var totalSql = @"
                                            select count(*) 
                                            from (
                                            select  	agent9.`Name` as XXXX,
	                                            agent9.`PathId` as YYYY
                                            from `agent` as agent9 
	                                            inner join `agentinventoryrecord` as record9
		                                            on agent9.`Id`=record9.`AgentId`
                                            where  agent9.`AgentLevel`=@AgentLevel
                                                     ) temp;
                                        ";

            var dataSql = @"
                                            select 	agent9.`Name` as XXXX,
	                                            agent9.`PathId` as YYYY
                                            from `agent` as agent9 
	                                            inner join `agentinventoryrecord` as record9
		                                            on agent9.`Id`=record9.`AgentId`
                                            where  agent9.`AgentLevel`=@AgentLevel
                                            order by agent9.`Id` desc
                                            limit 0,10;
                                        ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.MySQL_Int}
            };

            var res1 = await Conn.QueryPagingAsync<AgentVM>(1, 10, totalSql, dataSql, paras);

            Assert.True(res1.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }


    }
}
