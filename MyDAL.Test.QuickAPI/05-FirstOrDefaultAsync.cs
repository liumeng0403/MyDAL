using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QuickAPI
{
    public class _05_FirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            var xx7 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var option7 = new AlipayPaymentQueryOption();
            option7.CreatedOn = DateTime.Parse("2018-08-20 19:12:05.933786");
            var res7 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(option7);
            Assert.True(res7.Id == pk);

            var tuple7 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx8 = "";

            var res8 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7);
            Assert.True(res8.Id == pk);

            var tuple8 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx9 = "";

            var res9 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7, record => new AlipayPaymentRecordVM
            {
                Id = record.Id,
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res9.Id == pk);

            var tuple9 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx = "";

        }
    }
}
