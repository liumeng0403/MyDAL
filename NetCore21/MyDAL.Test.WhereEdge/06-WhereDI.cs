using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
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
                xx = string.Empty;

                if (!await Conn
                        .Queryer<PlatformMonthlyPerformance>()
                        .Where(it => it.Year == vm.Year)
                            .And(it => it.Month == vm.Month)
                        .IsExistAsync())
                {
                    Assert.True(true);
                }

                tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);
            }
        }

        public IMethodParamsTest ExistXTest { get; set; }

        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            var vm = new ApplyStockholderAwardAccountingVM();
            vm.Year = 2018;
            vm.Month = Month.October;

            ExistXTest = new InterfaceDI();
            await ExistXTest.eee(vm);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*****************************************************************************************/

        }
    }
}
