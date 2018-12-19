using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryVmColumn
{
    public class _05_AllAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;

            var res1 = await Conn
                .Queryer(out Agent agent1, out AgentInventoryRecord record1)
                .From(() => agent1)
                    .InnerJoin(() => record1)
                        .On(() => agent1.Id == record1.AgentId)
                .AllAsync(() => new AgentVM
                {
                    XXXX = agent1.Name,
                    YYYY = record1.Id.ToString()
                });

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
