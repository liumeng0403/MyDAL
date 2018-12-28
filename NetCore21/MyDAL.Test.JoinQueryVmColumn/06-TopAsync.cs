using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.JoinQueryVmColumn
{
    public class _06_TopAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            /*******************************************************************************************************************************/

            var xx7 = string.Empty;

            var res7 = await Conn
                .Queryer(out Agent agent7, out AgentInventoryRecord record7)
                .From(() => agent7)
                    .InnerJoin(() => record7)
                        .On(() => agent7.Id == record7.AgentId)
                .Where(() => record7.CreatedOn >= WhereTest.CreatedOn)
                .TopAsync(25, () => new AgentVM
                {
                    nn = agent7.PathId,
                    yy = record7.Id,
                    xx = agent7.Id,
                    zz = agent7.Name,
                    mm = record7.LockedCount
                });
            Assert.True(res7.Count == 25);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*******************************************************************************************************************************/

            xx = string.Empty;
            
        }
    }
}
