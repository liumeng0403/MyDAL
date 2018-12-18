using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.ShortcutAPI
{
    public class _02_ListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {
            /****************************************************************************************/

            var xx1 = "";

            var date = DateTime.Parse("2018-08-20");
            var res1 = await Conn.ListAsync<AlipayPaymentRecord>(it => it.CreatedOn >= date);
            Assert.True(res1.Count == 29);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.ListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= date);
            Assert.True(res1.Count == 29);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx3 = "";

            var res3 = await Conn.ListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.CreatedOn >= date,
                it => new AlipayPaymentRecordVM
                {
                    TotalAmount = it.TotalAmount,
                    Description = it.Description
                });
            Assert.True(res3.Count == 29);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx4 = "";

            var option4 = new AlipayPaymentQueryOption();
            option4.StartTime = date;
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

            var res6 = await Conn.ListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4,
                record => new AlipayPaymentRecordVM
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
