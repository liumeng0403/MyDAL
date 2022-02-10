using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _01_Char_LengthTest : TestBase
    {

        [Fact]
        public async Task Char_LengthTest()
        {

            /*
             *char_length
             */
            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Length > 2)
                .QueryListAsync();
            Assert.True(res1.Count == 22660);

            

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var resR1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => 2 < it.Name.Length)
                .QueryListAsync();
            Assert.True(res1.Count == resR1.Count);
            Assert.True(res1.Count == 22660);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res2 = await MyDAL_TestDB
                .Selecter(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name.Length > 2)
                .QueryListAsync<Agent>();
            Assert.True(res2.Count == 457);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res3 = await MyDAL_TestDB
                .Selecter(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Length > 2)
                .OrderBy(() => agent3.Name.Length)
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 457);

            

            /************************************************************************************************************************/

            xx = string.Empty;
        }


    }
}
