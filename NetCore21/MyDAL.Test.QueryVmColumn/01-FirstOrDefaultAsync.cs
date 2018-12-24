using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _01_FirstOrDefaultAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx3 = "";

            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .FirstOrDefaultAsync(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });
            Assert.Equal("樊士芹", res3.XXXX);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************/

            var xx = "";

        }
    }
}
