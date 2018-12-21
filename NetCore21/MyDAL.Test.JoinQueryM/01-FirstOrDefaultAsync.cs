using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryM
{
    public class _01_FirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx6 = "";

            var guid6 = Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef");
            var res6 = await Conn
                .Queryer(out Agent agent6, out AgentInventoryRecord record6)
                .From(() => agent6)
                    .InnerJoin(() => record6)
                        .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.Id == guid6)
                .FirstOrDefaultAsync<Agent>();
            Assert.NotNull(res6);
            Assert.Equal("夏明君", res6.Name);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx = "";

        }
    }
}
