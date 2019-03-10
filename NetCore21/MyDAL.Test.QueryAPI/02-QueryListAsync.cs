using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _02_QueryListAsync
        : TestBase
    {

        [Fact]
        public async Task History_01()
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
        public async Task Mock_QueryAllData()
        {
            xx = string.Empty;

            var res1 = await Conn.QueryListAsync<Agent>(null);

            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res2 = await Conn.QueryListAsync<Agent, AgentVM>(null);

            Assert.True(res2.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx3 = string.Empty;

            var res3 = await Conn.QueryListAsync<Agent, Guid>(null, it => it.Id);

            Assert.True(res3.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

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

        }

        [Fact]
        public async Task QueryVM_ST()
        {

        }

        [Fact]
        public async Task QueryVMColumn_ST()
        {

        }

        [Fact]
        public async Task QuerySingleColumn_MT()
        {

        }

        [Fact]
        public async Task QueryM_MT()
        {

        }

        [Fact]
        public async Task QueryVMColumn_MT()
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
