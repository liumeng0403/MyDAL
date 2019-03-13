using MyDAL.Test.Entities.MyDAL_TestDB;

namespace MyDAL.Test.DataRecovery
{
    public class _01_MySqlToSqlServer
        :TestBase
    {
        public void MySqlToSqlServer()
        {
            /*
             * AddressInfo
             */
             //var res1= Conn2.DeleteAsync<AddressInfo>()
            var addressInfos = Conn.QueryListAsync<AddressInfo>(null).GetAwaiter().GetResult();
            var res1 = Conn2.CreateBatchAsync(addressInfos).GetAwaiter().GetResult();
        }
    }
}
