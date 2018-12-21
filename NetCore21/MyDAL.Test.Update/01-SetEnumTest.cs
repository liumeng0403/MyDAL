using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Update
{
    public class _01_SetEnumTest : TestBase
    {
        [Fact]
        public async Task test()
        {
            var agent = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == Guid.Parse("040afaad-ae07-42fc-9dd0-0165443c847d"));

            /*****************************************************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Updater<Agent>()
                .Set(it => it.PathId, null)
                .Where(it => it.Id == agent.Id)
                .UpdateAsync();
            var res11 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == agent.Id);
            Assert.Null(res11.PathId);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            var xx2 = "";

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
                Assert.True(ex.Message.Equals("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", StringComparison.OrdinalIgnoreCase));
            }

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************************************************/

            var xx3 = "";

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

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res31 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == agent.Id);
            Assert.True(res31.PathId.Equals("xxxxxxx", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(res31.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

            var xx4 = "";

            agent.PathId = null;
            var res4 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
            {
                agent.PathId
            });

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res41 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == agent.Id);
            Assert.Null(res11.PathId);

            /*****************************************************************************************************************************************************************/

            var xx5 = "";

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
                Assert.True(ex.Message.Equals("NotAllowedNull -- 字段:[[PathId]]的值不能设为 Null !!!", StringComparison.OrdinalIgnoreCase));
            }

            /*****************************************************************************************************************************************************************/

            var xx6 = "";

            agent.PathId = "yyyyyyy";
            agent.ActiveOrderId = null;
                var res6 = await Conn.UpdateAsync<Agent>(it => it.Id == agent.Id, new
                {
                    agent.PathId,
                    agent.ActiveOrderId
                }, SetEnum.IgnoreNull);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res61 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == agent.Id);
            Assert.True(res61.PathId.Equals("yyyyyyy", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(res61.ActiveOrderId);

            /*****************************************************************************************************************************************************************/

            var xx = "";

        }
    }
}
