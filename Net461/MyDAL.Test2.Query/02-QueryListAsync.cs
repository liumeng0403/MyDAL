using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _02_QueryListAsync
        :TestBase
    {

        [TestMethod]
        public async Task QueryVMColumn_MT()
        {
            xx = string.Empty;

            var res1 = await Conn2
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => record.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                .QueryListAsync(() => new AgentVM
                {
                    nn = agent.PathId,
                    yy = record.Id,
                    xx = agent.Id,
                    zz = agent.Name,
                    mm = record.LockedCount
                });

            Assert.IsTrue(res1.Count == 574);
            Assert.IsTrue(res1.Any(it => "~00".Equals(it.nn, StringComparison.OrdinalIgnoreCase)));

            

            /*************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
