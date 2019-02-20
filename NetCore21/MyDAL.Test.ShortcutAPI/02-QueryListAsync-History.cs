using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _02_QueryListAsync_History
        : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var res1 = await Conn.QueryListAsync<Agent>();
            Assert.True(res1.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;

            var res2 = await Conn.QueryListAsync<Agent, AgentVM>();
            Assert.True(res2.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx3 = string.Empty;

            var res3 = await Conn.QueryListAsync<Agent, Guid>(it => it.Id);
            Assert.True(res3.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx4 = string.Empty;

            var res4 = await Conn.QueryListAsync<Agent, AgentVM>(it => new AgentVM
            {
                XXXX = it.Name,
                YYYY = it.PathId
            });
            Assert.True(res4.Count == 28620);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
