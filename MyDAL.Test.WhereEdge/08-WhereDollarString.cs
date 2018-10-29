using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _08_WhereDollarString : TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name == $"{"樊士芹"}")
                .QueryFirstOrDefaultAsync();
            Assert.Null(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            var xx2 = "";

            var name2 = "樊士芹";
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name == $"{name2}")
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            var xx3 = "";
            
            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >DateTime.Parse($"{WhereTest.CreatedOn.AddDays(-10)}"))
                .QueryListAsync();
            Assert.NotNull(res3);
            Assert.True(res3.Count == 28619);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            var xx4 = "";

            var name4 = "张";
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name.Contains($"{name4}%"))
                .QueryListAsync();
            Assert.NotNull(res4);
            Assert.True(res4.Count == 1996);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            var xx5 = "";
            
            var res5 = await Conn
                .Selecter<Agent>()
                .Where(it => it.PathId.Contains($"{WhereTest.ContainStr2}%"))
                .QueryListAsync();
            Assert.NotNull(res5);
            Assert.True(res5.Count == 20016);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*******************************************************************************************************************/

            var xx = "";
        }
    }
}
