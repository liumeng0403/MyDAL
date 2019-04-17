using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _10_Nullable_Bool
        : TestBase
    {
        [Fact]
        public async Task Bool_Default()
        {

        }

        [Fact]
        public async Task Bool_True()
        {

        }

        [Fact]
        public async Task Bool_False()
        {

        }

        [Fact]
        public async Task Not_Bool_Default()
        {

        }

        [Fact]
        public async Task Not_Bool_True()
        {

        }

        [Fact]
        public async Task Not_Bool_False()
        {

        }

        [Fact]
        public async Task Nullable_Bool_Default()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => it.IsDefault.Value)  //  true
                .QueryListAsync();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_True()
        {
            xx = string.Empty;

            var res1 = await Conn
                .Queryer<AddressInfo>()
                .Where(it => it.IsDefault.Value == true)  //  true
                .QueryListAsync();

            Assert.True(res1.Count == 5);
            Assert.True(res1.First().IsDefault);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task Nullable_Bool_False()
        {

        }

        [Fact]
        public async Task Not_Nullable_Bool_Default()
        {

        }

        [Fact]
        public async Task Not_Nullable_Bool_True()
        {

        }

        [Fact]
        public async Task Not_Nullable_Bool_False()
        {

        }
    }
}
