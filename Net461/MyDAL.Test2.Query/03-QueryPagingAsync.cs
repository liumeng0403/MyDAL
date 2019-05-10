using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _03_QueryPagingAsync
        : TestBase
    {
        [TestMethod]
        public async Task QueryVmColumn_MT()
        {

            xx = string.Empty;

            var res1 = await Conn2
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.IsTrue(res1.TotalCount == 574);

            

            xx = string.Empty;

        }

        [TestMethod]
        public async Task QueryVM_SQL()
        {
            xx = string.Empty;

            var totalSql = @"
                                            select count(*) 
                                            from (
                                            select  	agent9.[Name] as XXXX,
	                                            agent9.[PathId] as YYYY
                                            from [agent] as agent9 
	                                            inner join [agentinventoryrecord] as record9
		                                            on agent9.[Id]=record9.[AgentId]
                                            where  agent9.[AgentLevel]=@AgentLevel
                                                     ) temp;
                                        ";

            var dataSql = @"
                                            select 	agent9.[Name] as XXXX,
	                                            agent9.[PathId] as YYYY
                                            from [agent] as agent9 
	                                            inner join [agentinventoryrecord] as record9
		                                            on agent9.[Id]=record9.[AgentId]
                                            where  agent9.[AgentLevel]=@AgentLevel
                                            order by agent9.[Id] desc
                                            offset 0 rows fetch next 10 rows only;
                                        ";

            var paras = new List<XParam>
            {
                new XParam{ParamName="AgentLevel",ParamValue=AgentLevel.DistiAgent,ParamType= ParamTypeEnum.MySQL_Int}
            };

            var paging = new PagingResult<AgentVM>();
            paging.PageIndex = 1;
            paging.PageSize = 10;

            paging = await Conn2.QueryPagingAsync<AgentVM>(paging, totalSql, dataSql, paras);

            Assert.IsTrue(paging.Data.Count == 10);

            

            xx = string.Empty;

        }

    }
}
