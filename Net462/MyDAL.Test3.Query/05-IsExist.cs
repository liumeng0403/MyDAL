using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;

namespace MyDAL.Test3.Query
{
    [TestClass]
    public class _05_IsExist
        : TestBase
    {
        [TestMethod]
        public void IsExist_Shortcut()
        {
            xx = string.Empty;

            var date = DateTime.Parse("2018-08-20 20:33:21.584925");
            var id = Guid.Parse("89c9407f-7427-4570-92b7-0165590ac07e");

            var res1 = Conn3.IsExist<AlipayPaymentRecord>(it => it.CreatedOn == date && it.OrderId == id);

            Assert.IsTrue(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            xx = string.Empty;
        }
    }
}
