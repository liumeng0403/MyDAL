using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _03_QueryPagingAsync 
        : TestBase
    {
        [Fact]
        public async Task test()
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

            xx = string.Empty;

        }
    }
}