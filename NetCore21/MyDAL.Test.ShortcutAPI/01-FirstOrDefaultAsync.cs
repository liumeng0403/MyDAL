using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _01_FirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date= DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            var xx3 = string.Empty;

            var res3 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, Guid>(it => it.Id == pk && it.CreatedOn == date,it=>it.Id);
            Assert.True(res3==pk);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx4 = string.Empty;

            var res4 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
                it => new AlipayPaymentRecordVM
                {
                    Id = it.Id,
                    TotalAmount = it.TotalAmount,
                    Description = it.Description
                });
            Assert.NotNull(res4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx5 = string.Empty;

            var res5 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res5);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = string.Empty;

            var res6 = await Conn.FirstOrDefaultAsync<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);
            Assert.NotNull(res6);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/



            /****************************************************************************************/

            xx = string.Empty;

        }
    }
}
