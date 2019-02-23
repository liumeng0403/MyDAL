using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Update
{
    public class _01_SetEnumTest : TestBase
    {
        [Fact]
        public async Task test()
        {
            var agent = await Conn.QueryOneAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;

            var res1 = await Conn
                .Updater<Agent>()
                .Set(it => it.PathId, null)
                .Where(it => it.Id == agent.Id)
                .UpdateAsync();
            var res11 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Null(res11.PathId);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;

            try
            {
                var res2 = await Conn
                    .Updater<Agent>()
                    .Set(it => it.PathId, null)
                    .Where(it => it.Id == agent.Id)
                    .UpdateAsync(SetEnum.NotAllowedNull);
            }
            catch (Exception ex)
            {
                Assert.Equal("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", ex.Message, ignoreCase: true);
            }

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            var xx3 = string.Empty;

            agent.PathId = "xxxxxxx";
            agent.ActiveOrderId = null;
            var res3 = await Conn
                .Updater<Agent>()
                .Set(new
                {
                    agent.PathId,
                    agent.ActiveOrderId
                })
                .Where(it => it.Id == agent.Id)
                .UpdateAsync(SetEnum.IgnoreNull);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res31 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Equal("xxxxxxx", res31.PathId, ignoreCase: true);
            Assert.NotNull(res31.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

            var xx4 = string.Empty;

            agent.PathId = null;
            var res4 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
            {
                agent.PathId
            });

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res41 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Null(res11.PathId);

            /*****************************************************************************************************************************************************************/

            var xx5 = string.Empty;

            agent.PathId = null;
            try
            {
                var res5 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
                {
                    agent.PathId
                }, SetEnum.NotAllowedNull);
            }
            catch (Exception ex)
            {
                Assert.Equal("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", ex.Message, ignoreCase: true);
            }

            /*****************************************************************************************************************************************************************/

            var xx6 = string.Empty;

            agent.PathId = "yyyyyyy";
            agent.ActiveOrderId = null;
                var res6 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
                {
                    agent.PathId,
                    agent.ActiveOrderId
                }, SetEnum.IgnoreNull);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res61 = await Conn.QueryOneAsync<Agent>(it => it.Id == agent.Id);

            Assert.Equal("yyyyyyy", res61.PathId, ignoreCase: true);
            Assert.NotNull(res61.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
