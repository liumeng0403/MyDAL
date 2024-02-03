using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using Xunit;

namespace MyDAL.Compare
{
    public class _03_Equal
        : TestBase
    {

        [Fact]
        public void Equal()
        {

            xx = string.Empty;

            // == --> =
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .SelectList();

            Assert.True(res1.Count == 555);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public void NotEqual()
        {
            xx = string.Empty;

            // !(==) --> <>
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !(it.AgentLevel == AgentLevel.DistiAgent))
                .SelectList();

            Assert.True(res1.Count == 28064 || res1.Count == 28065);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            // != --> <>
            var res2 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel != AgentLevel.DistiAgent)
                .SelectList();

            Assert.True(res2.Count == 28064 || res2.Count == 28065);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
