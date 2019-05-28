using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Test2.Query
{
    [TestClass]
    public class _06_CountAsync
        :TestBase
    {

        [TestMethod]
        public async Task SqlAction_Distinct_Count_SpecialColumn_ST()
        {
            xx = string.Empty;

            var res1 = await Conn2
                .Queryer<Agent>()
                .Distinct()
                .CountAsync(it => it.AgentLevel);

            Assert.AreEqual(3, res1);

            xx = string.Empty;
        }

    }
}
