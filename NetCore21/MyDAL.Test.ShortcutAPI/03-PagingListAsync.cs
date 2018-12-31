using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _03_PagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            xx = string.Empty;

            var option1 = new AlipayPaymentPaggingQueryOption();
            option1.StartTime = DateTime.Parse("2018-08-20");
            var res1 = await Conn.PagingListAsync<AlipayPaymentRecord>(option1);
            Assert.True(res1.TotalCount == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

            var res2 = await Conn.PagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1);
            Assert.True(res2.TotalCount == 29);
            Assert.True(res2.Data.Count == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx=string.Empty;

            var res3 = await Conn.PagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res3.TotalCount == 29);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx=string.Empty;

        }
    }
}
