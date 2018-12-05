using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _02_ListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx4 = "";

            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .OrderBy(it => it.CreatedOn)
                .ListAsync<AgentVM>();

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx = "";
        }
    }
}
