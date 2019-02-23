using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _03_PagingListAsync
        : TestBase
    {
        [Fact]
        public async Task ST_QueryM_PagingOption()
        {

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            //   =    >=    <=
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(option1)
                .QueryPagingAsync();

            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task ST_QuerySingleColumn_PagingOption()
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
        public async Task ST_QueryVM_PagingOption()
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
        public async Task ST_QueryVmColumn_PagingOption()
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
    }
}
