using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.Func
{
    public class _02_TrimTest:TestBase
    {

        private async Task PreTrim()
        {
            var res1 = await Conn
                .Updater<Product>()
                .Set(it => it.Title, "  演示商品01  ")
                .Where(it => it.Id == Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f"))
                .UpdateAsync();
        }
        private async Task PreLTrim()
        {
            var res1 = await Conn
                .Updater<Product>()
                .Set(it => it.Title, "  演示商品01")
                .Where(it => it.Id == Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f"))
                .UpdateAsync();
        }
        private async Task PreRTrim()
        {
            var res1 = await Conn
                .Updater<Product>()
                .Set(it => it.Title, "演示商品01  ")
                .Where(it => it.Id == Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f"))
                .UpdateAsync();
        }

        [Fact]
        public async Task Test()
        {

            /******************************************************************************************************************/

            var xx1 = "";

            await PreTrim();
            var res1 = await Conn
                .Selecter<Product>()
                .Where(it => it.Title.Trim() == "演示商品01")
                .QueryFirstOrDefaultAsync();
            Assert.True(res1.Title == "  演示商品01  ");

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************************/

            var xx2 = "";

            await PreLTrim();
            var res2 = await Conn
                .Selecter<Product>()
                .Where(it => it.Title.TrimStart() == "演示商品01")
                .QueryFirstOrDefaultAsync();
            Assert.True(res2.Title == "  演示商品01");

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************************/

            var xx3 = "";

            await PreRTrim();
            var res3 = await Conn
                .Selecter<Product>()
                .Where(it => it.Title.TrimEnd() == "演示商品01")
                .QueryFirstOrDefaultAsync();
            Assert.True(res3.Title == "演示商品01  ");

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************************/

            var xx = "";

        }

    }
}
