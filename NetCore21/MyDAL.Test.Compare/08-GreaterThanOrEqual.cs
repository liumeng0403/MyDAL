using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _08_GreaterThanOrEqual
        : TestBase
    {
        [Fact]
        public async Task GreaterThanOrEqual()
        {
            xx = string.Empty;

            // >= --> >=
            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => agent1.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-90))
                .QueryListAsync<AgentInventoryRecord>();

            Assert.True(res1.Count == 574);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task NotGreaterThanOrEqual()
        {
            xx = string.Empty;

            // !(>=) --> <
            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .Where(() => !(agent1.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-90)))
                .QueryListAsync<AgentInventoryRecord>();

            Assert.True(res1.Count == 0);

            

            /******************************************************************************************************************************/
            xx = string.Empty;

            // < --> <
            var res2 = await Conn
                .Queryer(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.CreatedOn < Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-90))
                .QueryListAsync<AgentInventoryRecord>();

            Assert.True(res2.Count == 0);

            

            /******************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
