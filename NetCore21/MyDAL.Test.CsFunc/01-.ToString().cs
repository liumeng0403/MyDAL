using HPC.DAL;
using MyDAL.Test.Entities.MySql;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.CsFunc
{
    public class _01_
        : TestBase
    {

        [Fact]
        public async Task QueryOne_SingleColumn_ST()
        {
            await MySQL_PreData('A', 1);

            xx = string.Empty;



            xx = string.Empty;
        }

        [Fact]
        public async Task QueryOne_VmColumn_ST()
        {

        }

        [Fact]
        public async Task QueryOne_SingleColumn_MT()
        {

        }

        [Fact]
        public async Task QueryOne_VmColumn_MT()
        {

        }
    }
}
