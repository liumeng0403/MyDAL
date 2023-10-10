using Mysql.DataNet6;

namespace Function_Count
{
    [TestClass]
    public class Count
        : Net6Base
    {
        [TestMethod]
        public void TestMethod1()
        {
            var conn = MyDAL_TestDB;
        }
    }
}