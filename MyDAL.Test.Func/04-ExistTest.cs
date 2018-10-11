using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _04_ExistTest : TestBase
    {
        public class ApplyStockholderAwardAccountingVM
        {
            [Display(Name = "年")]
            public int Year { get; set; }

            [Display(Name = "月")]
            public Month Month { get; set; }
        }

        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            /*****************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************/

            var xx2 = "";

            var vm = new ApplyStockholderAwardAccountingVM();
            vm.Year = 2018;
            vm.Month = Month.October;
            var res2 = await Conn
                .Selecter<PlatformMonthlyPerformance>()
                .Where(it => it.Year==vm.Year)
                .And(it=>it.Month==vm.Month)
                .ExistAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************/

            var xx = "";
        }

    }
}
