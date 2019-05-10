using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.SqlAction
{
    public class _01_OrderBy
        : TestBase
    {
        [Fact]
        public async Task Queryer_OrderBy_QueryPaging_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .QueryPagingAsync(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Queryer_OrderBy_ThenOrderBy_QueryPaging_ST()
        {

        }

        [Fact]
        public async Task Where_OrderBy_QueryPaging_ST()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.StartsWith("张"))
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .QueryPagingAsync(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Where_OrderBy_ThenOrderBy_QueryPaging_ST()
        {

        }

        [Fact]
        public async Task Where_OrderBy_QueryPaging_MT()
        {

        }

        [Fact]
        public async Task Where_OrderBy_ThenOrderBy_QueryPaging_MT()
        {

        }
    }
}
