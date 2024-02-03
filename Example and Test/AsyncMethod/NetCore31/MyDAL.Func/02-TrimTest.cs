using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _02_TrimTest : TestBase
    {

        private void PreTrim()
        {
            var pk1 = Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f");
            var res1 = MyDAL_TestDB.Update<Product>(it => it.Id == pk1, new
            {
                Title = "  演示商品01  "
            });
        }
        private void PreLTrim()
        {
            var res1 = MyDAL_TestDB
                .Updater<Product>()
                .Set(it => it.Title, "  演示商品01")
                .Where(it => it.Id == Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f"))
                .Update();
        }
        private void PreRTrim()
        {
            var res1 = MyDAL_TestDB
                .Updater<Product>()
                .Set(it => it.Title, "演示商品01  ")
                .Where(it => it.Id == Guid.Parse("b3866d7c-2b51-46ae-85cb-0165c9121e8f"))
                .Update();
        }

        [Fact]
        public void Test()
        {

            /******************************************************************************************************************/

            xx = string.Empty;

            PreTrim();
            var res1 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.Title.Trim() == "演示商品01")
                .SelectOne();

            Assert.True(res1.Title == "  演示商品01  ");

            

            /******************************************************************************************************************/

            xx = string.Empty;

            PreLTrim();
            var res2 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.Title.TrimStart() == "演示商品01")
                .SelectOne();

            Assert.True(res2.Title == "  演示商品01");

            

            /******************************************************************************************************************/

            xx=string.Empty;

            PreRTrim();
            var res3 = MyDAL_TestDB
                .Selecter<Product>()
                .Where(it => it.Title.TrimEnd() == "演示商品01")
                .SelectOne();

            Assert.True(res3.Title == "演示商品01  ");

            

            /******************************************************************************************************************/

            xx=string.Empty;

        }

    }
}
