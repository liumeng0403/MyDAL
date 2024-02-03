using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using Xunit;

namespace MyDAL.SqlAction
{
    public class _01_OrderBy
        : TestBase
    {
        [Fact]
        public void Selecter_OrderBy_SelectPaging_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .SelectPaging(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public void Selecter_OrderBy_ThenOrderBy_SelectPaging_ST()
        {

        }

        [Fact]
        public void Where_OrderBy_SelectPaging_ST()
        {
            xx = string.Empty;

            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.StartsWith("张"))
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .SelectPaging(1, 10, it => it.Name);

            Assert.True(res1.Data.Count == 10);

            

            /*****************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public void Where_OrderBy_ThenOrderBy_SelectPaging_ST()
        {

        }

        [Fact]
        public void Where_OrderBy_SelectPaging_MT()
        {

        }

        [Fact]
        public void Where_OrderBy_ThenOrderBy_SelectPaging_MT()
        {

        }
    }
}
