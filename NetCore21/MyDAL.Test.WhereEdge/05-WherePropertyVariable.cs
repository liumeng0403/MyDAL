using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _05_WherePropertyVariable : TestBase
    {

        public Guid AgentId { get; set; }

        [Fact]
        public async Task Property()
        {
            xx = string.Empty;

            AgentId = Guid.Parse("00079c84-a511-418b-bd5b-0165442eb30a");
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == AgentId)
                .QueryOneAsync<AgentVM>();

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

    }
}
