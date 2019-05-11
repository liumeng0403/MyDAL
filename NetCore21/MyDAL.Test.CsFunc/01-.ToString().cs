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
            var pk = 'A';
            await MySQL_PreData(pk, 1);

            xx = string.Empty;

            var res1 = await Conn.QueryOneAsync<MySQL_EveryType>(it => it.Char == $"{pk}-char");

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
