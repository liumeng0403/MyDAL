using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _06_CountAsync
        : TestBase
    {
        [Fact]
        public async Task Count_Star_Shortcut()
        {
            xx = string.Empty;

            var res1 = await Conn.CountAsync<Agent>(it => it.Name.Length > 3);

            Assert.True(res1 == 116);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task Count_SpecialColumn_Shortcut()
        {

        }

        [Fact]
        public async Task Count_Star_ST()
        {

        }

        [Fact]
        public async Task Count_SpecialColumn_ST()
        {

        }

        [Fact]
        public async Task Count_Star_MT()
        {

        }

        [Fact]
        public async Task Count_SpecialColumn_MT()
        {

        }
    }
}
