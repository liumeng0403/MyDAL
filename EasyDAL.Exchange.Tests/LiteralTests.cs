using System.Linq;
using EasyDAL.Exchange.DynamicParameter;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class LiteralTests : TestBase
    {

        [Fact]
        public void LiteralReplacementBoolean()
        {
            var row = connection.Query<int?>("select 42 where 1 = {=val}", new { val = true }).SingleOrDefault();
            Assert.NotNull(row);
            Assert.Equal(42, row);
            row = connection.Query<int?>("select 42 where 1 = {=val}", new { val = false }).SingleOrDefault();
            Assert.Null(row);
        }

        [Fact]
        public void LiteralReplacementWithIn()
        {
            var data = connection.Query<MyRow>("select @x where 1 in @ids and 1 ={=a}",
                new { x = 1, ids = new[] { 1, 2, 3 }, a = 1 }).ToList();
        }

        private class MyRow
        {
            public int x { get; set; }
        }






    }
}
