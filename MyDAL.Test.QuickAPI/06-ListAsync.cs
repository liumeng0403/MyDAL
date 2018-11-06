using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QuickAPI
{
    public class _06_ListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx4 = "";

            var option4 = new AlipayPaymentQueryOption();
            option4.StartTime = DateTime.Parse("2018-08-20");
            var res4 = await Conn.ListAsync<AlipayPaymentRecord>(option4);
            Assert.True(res4.Count == 29);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.ListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4);
            Assert.True(res4.Count == 29);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = "";

            var res6 = await Conn.ListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res4.Count == 29);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

        }
    }
}
