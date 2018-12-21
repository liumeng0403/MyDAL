using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.ShortcutAPI
{
    public class _01_FirstOrDefault:TestBase
    {
        [Fact]
        public async Task test()
        {

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            var xx3 = "";

            var res3 = Conn.FirstOrDefault<AlipayPaymentRecord, Guid>(it => it.Id == pk && it.CreatedOn == date, it => it.Id);
            Assert.True(res3 == pk);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx4 = "";

            var res4 = Conn.FirstOrDefault<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
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

            var res5 = Conn.FirstOrDefault<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res5);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = "";

            var res6 = Conn.FirstOrDefault<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res6);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/



            /****************************************************************************************/

            var xx = "";

        }
    }
}
