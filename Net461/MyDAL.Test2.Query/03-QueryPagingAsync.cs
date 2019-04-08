using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _03_QueryPagingAsync
        : TestBase
    {
        [TestMethod]
        public async Task QueryVmColumn_MT()
        {

            xx = string.Empty;

            var res1 = await Conn2
                .Queryer(out Agent agent, out AgentInventoryRecord record)
                .From(() => agent)
                    .InnerJoin(() => record)
                        .On(() => agent.Id == record.AgentId)
                .Where(() => agent.AgentLevel == AgentLevel.DistiAgent)
                .QueryPagingAsync(1, 10, () => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            Assert.IsTrue(res1.TotalCount == 574);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

        }
    }
}
