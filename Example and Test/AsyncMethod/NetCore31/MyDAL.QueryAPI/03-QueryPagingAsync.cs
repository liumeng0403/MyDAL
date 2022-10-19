using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.QueryAPI
{
    public class _03_SelectPagingAsync
        : TestBase
    {

        [Fact]
        public async Task History_02()
        {

            /******************************************************************************************************/

            xx = string.Empty;

            // order by
            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)128)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .SelectPagingAsync(1, 10);

            Assert.True(res1.TotalCount == 555);

            

            /******************************************************************************************************/

            xx = string.Empty;

            // key
            var res2 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .SelectPagingAsync(1, 10);

            Assert.True(res2.TotalPage == 2807);

            

            /******************************************************************************************************/

            xx = string.Empty;

            var res4 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .SelectPagingAsync(1, 10);

            

            var resR4 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .SelectPagingAsync(1, 10);

            Assert.True(res4.TotalCount == resR4.TotalCount);
            Assert.True(res4.TotalCount == 28619);

            

            /*************************************************************************************************************************/

        }

        [Fact]
        public async Task History_03()
        {

            xx = string.Empty;

            // where method -- option orderby 
            var res7 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name == "樊士芹")
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .SelectPagingAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res7.Data.Count == 1);

            

            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task History_04()
        {

            xx = string.Empty;

            var res11 = await MyDAL_TestDB
                .Selecter(out Agent agent11, out AgentInventoryRecord record11)
                .From(() => agent11)
                    .InnerJoin(() => record11)
                        .On(() => agent11.Id == record11.AgentId)
                .Where(()=>agent11.AgentLevel== AgentLevel.DistiAgent)
                .SelectPagingAsync(5,10,() => new AgentVM
                {
                    XXXX = agent11.Name,
                    YYYY = agent11.PathId
                });

            Assert.True(res11.TotalCount == 574);
            Assert.True(res11.PageSize == 10);
            Assert.True(res11.PageIndex == 5);
            Assert.True(res11.Data.Count == 10);

            

        }

        [Fact]
        public async Task History_05()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            // 无条件
            var res13 = await MyDAL_TestDB
                .Selecter<Agent>()
                .SelectPagingAsync<AgentVM>(1, 10);

            Assert.True(res13.TotalCount == 28620);
            Assert.True(res13.Data.Count == 10);

            

            /*************************************************************************************************************************/


            xx = string.Empty;

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task Mock_SelectAllPaging_SelectSingleColumn_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .SelectPagingAsync(1, 10, it => it.Id);

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_SelectAllPaging_SelectM_ST()
        {

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res3 = await MyDAL_TestDB
                .Selecter<Agent>()
                .SelectPagingAsync(1, 10);

            Assert.True(res3.TotalCount == 28620);

            

        }

        [Fact]
        public async Task Mock_SelectAllPaging_SelectVM_ST()
        {

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res7 = await MyDAL_TestDB
                .Selecter<Agent>()
                .SelectPagingAsync<AgentVM>(1, 10);

            Assert.True(res7.TotalCount == 28620);

            

            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_SelectAllPaging_SelectVmColumn_ST()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .SelectPagingAsync(1, 10, it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res1.Data.Count == 10);
            Assert.True(res1.TotalCount == 28620);

            

            xx = string.Empty;
        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task SelectSingleColumn_ST()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .SelectPagingAsync(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            

            /*****************************************************************************************************************************/

        }

        [Fact]
        public async Task SelectM_ST()
        {

            xx = string.Empty;

            // 
            var res1 = await MyDAL_TestDB
                .Selecter<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .SelectPagingAsync(1, 10);

            Assert.True(res1.TotalPage == 56);

            xx = string.Empty;
        }

        [Fact]
        public async Task SelectVM_ST()
        {

            xx = string.Empty;

            var res6 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SelectPagingAsync<AgentVM>(1, 10);

            

            var resR6 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) <= it.CreatedOn)
                .SelectPagingAsync<AgentVM>(1, 10);

            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            


            /*************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task SelectVmColumn_ST()
        {

            xx = string.Empty;

            var res8 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .SelectPagingAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res8.TotalCount == 28619);

            

            /*************************************************************************************************************************/

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task Mock_NoneCondition_SelectSingleColumn_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .SelectPagingAsync(1, 10, () => agent.Name);

            Assert.True(res1.TotalCount == 574);

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_NoneCondition_SelectM_MT()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .SelectPagingAsync<Agent>(1, 10);

            Assert.True(res1.TotalCount == 574);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task Mock_NoneCondition_SelectVmColumn_MT()
        {
            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .SelectPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res1.TotalCount == 574);

            

            xx = string.Empty;
        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task SelectSingleColumn_MT()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.AgentLevel == AgentLevel.DistiAgent)
                .SelectPagingAsync(1, 10, () => agent1.Id);

            Assert.True(res1.TotalCount == 574);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task SelectM_MT()
        {

            xx = string.Empty;

            var res5 = await MyDAL_TestDB
                .Selecter(out Agent agent5, out AgentInventoryRecord record5)
                .From(() => agent5)
                    .InnerJoin(() => record5)
                        .On(() => agent5.Id == record5.AgentId)
                .Where(() => agent5.AgentLevel == AgentLevel.DistiAgent)
                .SelectPagingAsync<Agent>(1, 10);

            Assert.True(res5.TotalCount == 574);

            

            /*************************************************************************************************************************/

        }

        [Fact]
        public async Task SelectVmColumn_MT()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Selecter(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .SelectPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.True(res1.TotalCount == 574);

            

            xx = string.Empty;

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task SelectSingleColumn_SQL()
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
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.Int_MySQL_SqlServer}
            };

            var paging = new PagingResult<Guid>();
            paging.PageIndex = 1;
            paging.PageSize = 10;

            paging.TotalCount = MyDAL_TestDB.SelectOne<int>(totalSql, paras);
            paging.Data = await MyDAL_TestDB.SelectListAsync<Guid>(dataSql, paras);

            Assert.True(paging.TotalPage == 58);

            

            xx = string.Empty;

        }

        [Fact]
        public async Task SelectVM_SQL()
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
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.Int_MySQL_SqlServer}
            };

            var paging = new PagingResult<AgentVM>();
            paging.PageIndex = 1;
            paging.PageSize = 10;

            paging.TotalCount = MyDAL_TestDB.SelectOne<int>(totalSql, paras);
            paging.Data = await MyDAL_TestDB.SelectListAsync<AgentVM>(dataSql, paras);

            Assert.True(paging.Data.Count == 10);

            xx = string.Empty;

        }


    }
}
