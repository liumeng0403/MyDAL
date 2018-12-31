using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _02_TrimTest : TestBase
    {

        private async Task PreTrim()
        {
            var pk1 = Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f");
            var res1 = await Conn.UpdateAsync<Product>(it => it.Id == pk1, new
            {
                Title = "  演示商品01  "
            });
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

            xx = string.Empty;

            await PreTrim();
            var res1 = await Conn
                .Queryer<Product>()
                .Where(it => it.Title.Trim() == "演示商品01")
                .FirstOrDefaultAsync();
            Assert.True(res1.Title == "  演示商品01  ");

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************************/

            xx = string.Empty;

            await PreLTrim();
            var res2 = await Conn
                .Queryer<Product>()
                .Where(it => it.Title.TrimStart() == "演示商品01")
                .FirstOrDefaultAsync();
            Assert.True(res2.Title == "  演示商品01");

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************************/

            xx=string.Empty;

            await PreRTrim();
            var res3 = await Conn
                .Queryer<Product>()
                .Where(it => it.Title.TrimEnd() == "演示商品01")
                .FirstOrDefaultAsync();
            Assert.True(res3.Title == "演示商品01  ");

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************************/

            xx=string.Empty;

        }

    }
}
