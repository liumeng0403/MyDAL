using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.JoinQueryVmColumn
{
    public class _02_ListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************************************************************/

            xx = string.Empty;

            var res12 = await Conn
                .Queryer(out Agent agent12, out AgentInventoryRecord record12)
                .From(() => agent12)
                    .InnerJoin(() => record12)
                        .On(() => agent12.Id == record12.AgentId)
                .Where(() => record12.CreatedOn >= WhereTest.CreatedOn)
                .ListAsync(() => new AgentVM
                {
                    nn = agent12.PathId,
                    yy = record12.Id,
                    xx = agent12.Id,
                    zz = agent12.Name,
                    mm = record12.LockedCount
                });
            Assert.True(res12.Count == 574);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);
            var yy2 = res12.First().nn;

            /*************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
