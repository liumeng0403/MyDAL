using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Interfaces;
using MyDAL.Test.ViewModels;
using Xunit;

namespace MyDAL.WhereEdge
{
    public class _06_WhereDI : TestBase
    {
        public class InterfaceDI : TestBase, IMethodParamsTest
        {

            // 在接口注入中使用 勿删!!!
            public void eee(ApplyStockholderAwardAccountingVM vm)
            {
                xx = string.Empty;

                if (!MyDAL_TestDB
                        .Selecter<PlatformMonthlyPerformance>()
                        .Where(it => it.Year == vm.Year)
                            .And(it => it.Month == vm.Month)
                        .IsExist())
                {
                    Assert.True(true);
                }

                
            }
        }

        public IMethodParamsTest ExistXTest { get; set; }

        [Fact]
        public void test()
        {

            xx = string.Empty;

            var vm = new ApplyStockholderAwardAccountingVM();
            vm.Year = 2018;
            vm.Month = Month.October;

            ExistXTest = new InterfaceDI();
            ExistXTest.eee(vm);

            

            /*****************************************************************************************/

        }
    }
}
