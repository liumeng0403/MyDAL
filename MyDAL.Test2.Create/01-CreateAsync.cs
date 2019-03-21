using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.TestData;
using System.Threading.Tasks;

namespace MyDAL.Test2.Create
{
    [TestClass]
    public class _01_CreateAsync
        :TestBase
    {
        
        [TestMethod]
        public async Task CreateMulti()
        {

            var list = await new CreateData().PreCreateBatch(Conn2);

            xx = string.Empty;

            var res1 = await Conn2
                .Creater<AddressInfo>()
                .CreateBatchAsync(list);

            Assert.IsTrue(res1 == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
