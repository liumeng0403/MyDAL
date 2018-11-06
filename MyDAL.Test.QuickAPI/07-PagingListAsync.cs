using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuickAPI
{
    public class _07_PagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            var xx1 = "";

            var option1 = new AlipayPaymentPaggingQueryOption();
            option1.StartTime = DateTime.Parse("2018-08-20");
            var res1 = await Conn.PagingListAsync<AlipayPaymentRecord>(option1);
            Assert.True(res1.TotalCount == 29);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.PagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1);
            Assert.True(res2.TotalCount == 29);
            Assert.True(res2.Data.Count == 10);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx3 = "";

            var res3 = await Conn.PagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res3.TotalCount == 29);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx = "";

        }
    }
}
