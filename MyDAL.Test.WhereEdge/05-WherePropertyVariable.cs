using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.WhereEdge
{
    public class _05_WherePropertyVariable:TestBase
    {

        public Guid AgentId { get; set; }

        [Fact]
        public async Task Property()
        {
            var xx1 = "";

            AgentId = Guid.Parse("00079c84-a511-418b-bd5b-0165442eb30a");
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == AgentId)
                .QueryFirstOrDefaultAsync<AgentVM>();
            Assert.NotNull(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
