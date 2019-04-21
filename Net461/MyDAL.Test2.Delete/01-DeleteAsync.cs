using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;

namespace MyDAL.Test2.Delete
{
    [TestClass]
    public class _01_DeleteAsync
        :TestBase
    {

        [TestMethod]
        public async Task Mock_DeleteAll_ST()
        {
            xx = string.Empty;

            var res1 = await Conn2.DeleteAsync<AddressInfo>(it=>true);

            Assert.IsTrue(res1 == 7);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

    }
}
