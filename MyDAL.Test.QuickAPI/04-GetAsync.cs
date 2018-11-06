using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuickAPI
{
    public class _04_GetAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx10 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var res10 = await Conn.GetAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(pk, record => new AlipayPaymentRecordVM
            {
                Id = record.Id,
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.NotNull(res10);

            var tuple10 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx11 = "";

            var res11 = await Conn.GetAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(pk);
            Assert.NotNull(res11);

            var tuple11 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx12 = "";

            var res12 = await Conn.GetAsync<AlipayPaymentRecord>(pk);
            Assert.NotNull(res12);

            var tuple12 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

        }
    }
}
