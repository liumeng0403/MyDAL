using MyDAL;
using Mysql.Data_Net6.Tables;
using Mysql.DataNet6;

namespace Function_Count
{
    [TestClass]
    public class Count
        : Net6Base
    {
        [TestMethod]
        public void 单表_指定列()
        {
            try
            {
                var db = DB_MyDAL_DEV;
                
                var res1 = db.OpenDebug()
                    .Selecter<Agent>()
                    .Where(it => it.Name.Contains("陈%"))
                    .SelectOne(it => XFunction.COUNT(it.Id));
                
                Assert.IsTrue(res1 > 100);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, "msg=" + ex.Message);
            }
        }
    }
}