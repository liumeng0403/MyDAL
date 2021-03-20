using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.QueryAPI
{
    public class _05_IsExistAsync
        : TestBase
    {

        [Fact]
        public async Task IsExist_Shortcut()
        {
            xx = string.Empty;

            var date = DateTime.Parse("2018-08-20 20:33:21.584925");
            var id = Guid.Parse("89c9407f-7427-4570-92b7-0165590ac07e");

            // 判断 AlipayPaymentRecord 表中是否存在符合条件的数据
            bool res1 = await MyDAL_TestDB.IsExistAsync<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);

            Assert.True(res1);

            

            /********************************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Mock_TableIsHavingData_ST()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer<Agent>()
                .IsExistAsync();

            Assert.True(res1);

            

            /*****************************************************************************************/

        }

        [Fact]
        public async Task IsExist_ST()
        {

            xx = string.Empty;

            // 判断 Agent 表 中 是否存在符合条件的数据
            var res1 = await MyDAL_TestDB
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("002c1ca9-f2df-453a-87e0-0165443dcc31"))
                .IsExistAsync();

            Assert.True(res1);

            

            /*****************************************************************************************/

        }

        [Fact]
        public async Task Mock_JoinNoneConditionIsHavingData_MT()
        {

            xx = string.Empty;

            var res1 = await MyDAL_TestDB
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .IsExistAsync();

            Assert.True(res1);

            

            /*****************************************************************************************/

        }

        [Fact]
        public async Task IsExist_MT()
        {
            xx = string.Empty;

            //
            bool res1 = await MyDAL_TestDB
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.Id == Guid.Parse("002c1ca9-f2df-453a-87e0-0165443dcc31"))
                .IsExistAsync();

            Assert.True(res1);

            

            /*****************************************************************************************/

            xx = string.Empty;
        }

    }
}
