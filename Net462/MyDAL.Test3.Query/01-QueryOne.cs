using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;

namespace MyDAL.Test3.Query
{
    [TestClass]
    public class _01_QueryOne
        : TestBase
    {
        [TestMethod]
        public void QuerySingleColumn_Shortcut()
        {

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            xx = string.Empty;

            var res1 = Conn3.QueryOne<AlipayPaymentRecord, Guid>(it => it.Id == pk && it.CreatedOn == date, it => it.Id);

            Assert.IsTrue(res1 == pk);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        [TestMethod]
        public void QueryM_Shortcut()
        {

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            xx = string.Empty;

            var res1 = Conn3.QueryOne<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);

            Assert.IsNotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        [TestMethod]
        public void QueryVM_Shortcut()
        {

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            xx = string.Empty;

            var res1 = Conn3.QueryOne<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);

            Assert.IsNotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

        }

        [TestMethod]
        public void QueryVMColumn_Shortcut()
        {
            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            /****************************************************************************************/

            xx = string.Empty;

            var res1 = Conn3.QueryOne<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
            it => new AlipayPaymentRecordVM
            {
                Id = it.Id,
                TotalAmount = it.TotalAmount,
                Description = it.Description
            });

            Assert.IsNotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;
        }
    }
}
