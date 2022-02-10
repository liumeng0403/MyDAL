using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _03_Equal
        : TestBase
    {

        [Fact]
        public async Task Equal()
        {

            xx = string.Empty;

            // == --> =
            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();

            Assert.True(res1.Count == 555);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task NotEqual()
        {
            xx = string.Empty;

            // !(==) --> <>
            var res1 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !(it.AgentLevel == AgentLevel.DistiAgent))
                .QueryListAsync();

            Assert.True(res1.Count == 28064);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            // != --> <>
            var res2 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel != AgentLevel.DistiAgent)
                .QueryListAsync();

            Assert.True(res2.Count == 28064);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
