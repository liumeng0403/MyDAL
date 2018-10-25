using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _03_QueryPagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx8 = "";

            var res8 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            var tuple8 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

        }
    }
}