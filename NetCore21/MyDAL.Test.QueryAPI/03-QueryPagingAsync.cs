using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
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

        }

        /*********************************************************************************************************************************************************/

        [Fact]
        public async Task QuerySingleColumn_MT()
        {

        }

        [Fact]
        public async Task QueryM_MT()
        {

        }

        [Fact]
        public async Task QueryVM_MT()
        {

        }

        [Fact]
        public async Task QueryVmColumn_MT()
        {

        }


        [Fact]
        public async Task QuerySingleColumn_SQL()
        {

        }

        [Fact]
        public async Task QueryVM_SQL()
        {

        }


    }
}
