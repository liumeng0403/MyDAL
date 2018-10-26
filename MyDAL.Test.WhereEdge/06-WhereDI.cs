using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Interfaces;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _06_WhereDI : TestBase
    {
        public class InterfaceDI : TestBase, IMethodParamsTest
        {

            // 在接口注入中使用 勿删!!!
            public async Task eee(ApplyStockholderAwardAccountingVM vm)
            {
                var xx2 = "";

                if (!await Conn
                        .Selecter<PlatformMonthlyPerformance>()
                        .Where(it => it.Year == vm.Year)
                        .And(it => it.Month == vm.Month)
                        .ExistAsync())
                {
                    Assert.True(true);
                }

                var tuple2 = (XDebug.SQL, XDebug.Parameters);
            }
        }

        public IMethodParamsTest ExistXTest { get; set; }

        [Fact]
        public async Task test()
        {

            var xx2 = "";

            var vm = new ApplyStockholderAwardAccountingVM();
            vm.Year = 2018;
            vm.Month = Month.October;

            ExistXTest = new InterfaceDI();
            await ExistXTest.eee(vm);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************/

        }
    }
}
