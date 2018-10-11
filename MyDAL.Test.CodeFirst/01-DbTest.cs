using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.CodeFirst
{
    public class _01_DbTest:TestBase
    {

        [Fact]
        public async Task DbCreateTest()
        {

            /**************************************************************************************************************/

            var xx1 = "";

            // test 重复 code first 
            var res1 = await Conn3
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();
            var res11 = await Conn3
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();

            /**************************************************************************************************************/

            var xx = "";

        }

    }
}
