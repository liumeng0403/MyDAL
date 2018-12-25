using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.WhereEdge
{
    public class _08_WhereDollarString : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx = string.Empty;

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name == $"{"樊士芹"}")
                .FirstOrDefaultAsync();
            Assert.NotNull(res1);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var name2 = "樊士芹";
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name == $"{name2}")
                .FirstOrDefaultAsync();
            Assert.NotNull(res2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn > DateTime.Parse($"{WhereTest.CreatedOn.AddDays(-10)}"))
                .ListAsync();
            Assert.NotNull(res3);
            Assert.True(res3.Count == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var name4 = "张";
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains($"{name4}%"))
                .ListAsync();
            Assert.NotNull(res4);
            Assert.True(res4.Count == 1996);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.PathId.Contains($"{WhereTest.ContainStr2}%"))
                .ListAsync();
            Assert.NotNull(res5);
            Assert.True(res5.Count == 20016);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;

            var like61 = "李";
            var like62 = "张";
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains($"{like61}%") || it.Name.Contains($"{like62}%"))
                .ListAsync();
            var res61 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains($"{like61}%"))
                .ListAsync();
            var res62 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name.Contains($"{like62}%"))
                .ListAsync();
            Assert.True(res61.Count != 0);
            Assert.True(res62.Count != 0);
            Assert.True(res6.Count == res61.Count + res62.Count);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
