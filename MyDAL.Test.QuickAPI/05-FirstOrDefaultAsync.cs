using MyDAL.Test.Entities.EasyDal_Exchange;
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

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date= DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            var xx4 = "";

            var res4 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
                it => new AlipayPaymentRecordVM
                {
                    Id = it.Id,
                    TotalAmount = it.TotalAmount,
                    Description = it.Description
                });
            Assert.NotNull(res4);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res5);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = "";

            var res6 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res6);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            //var xx7 = "";

            //var option7 = new AlipayPaymentQueryOption();
            //option7.CreatedOn = date;
            //var res7 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(option7);
            //Assert.True(res7.Id == pk);

            //var tuple7 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            //var xx8 = "";

            //var res8 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7);
            //Assert.True(res8.Id == pk);

            //var tuple8 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            //var xx9 = "";

            //var res9 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7, record => new AlipayPaymentRecordVM
            //{
            //    Id = record.Id,
            //    TotalAmount = record.TotalAmount,
            //    Description = record.Description
            //});
            //Assert.True(res9.Id == pk);

            //var tuple9 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx = "";

        }
    }
}
