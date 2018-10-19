using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuickAPI
{
    public class _01_QuickApiTest : TestBase
    {
        private class AlipayPaymentPaggingQueryOption : PagingQueryOption
        {
            [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
            public DateTime StartTime { get; set; }
        }
        private class AlipayPaymentQueryOption : QueryOption
        {
            [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
            public DateTime StartTime { get; set; }

            public DateTime CreatedOn { get; set; }
        }

        [Fact]
        public async Task Test1()
        {
            /****************************************************************************************/

            var xx1 = "";

            var option1 = new AlipayPaymentPaggingQueryOption();
            option1.StartTime = DateTime.Parse("2018-08-20");
            var res1 = await Conn.QueryPagingListAsync<AlipayPaymentRecord>(option1);
            Assert.True(res1.TotalCount == 29);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.QueryPagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1);
            Assert.True(res2.TotalCount == 29);
            Assert.True(res2.Data.Count == 10);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx3 = "";

            var res3 = await Conn.QueryPagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res3.TotalCount == 29);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx4 = "";

            var option4 = new AlipayPaymentQueryOption();
            option4.StartTime = DateTime.Parse("2018-08-20");
            var res4 = await Conn.QueryListAsync<AlipayPaymentRecord>(option4);
            Assert.True(res4.Count == 29);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4);
            Assert.True(res4.Count == 29);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = "";

            var res6 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res4.Count == 29);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx7 = "";

            var option7 = new AlipayPaymentQueryOption();
            option7.CreatedOn = DateTime.Parse("2018-08-20 19:12:05.933786");
            var res7 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(option7);
            Assert.True(res7.Id == Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"));

            var tuple7 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx8 = "";
            
            var res8 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord,AlipayPaymentRecordVM>(option7);
            Assert.True(res8.Id == Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"));

            var tuple8 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx9 = "";

            var res9 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7,record=>new AlipayPaymentRecordVM
            {
                Id=record.Id,
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res9.Id == Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"));

            var tuple9 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx = "";

        }
    }
}
