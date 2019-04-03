using MyDAL.Test.Entities.MyDAL_TestDB;

namespace MyDAL.Test.DataRecovery
{
    public class _01_MySqlToSqlServer
        :TestBase
    {

        public void MySqlToSqlServer2008R2Plus()
        {

            /*
             * AddressInfo
             */

            var sqlDelete01 = Conn2.DeleteAsync<AddressInfo>(null).GetAwaiter().GetResult();
            var dataList01 = Conn.QueryListAsync<AddressInfo>(null).GetAwaiter().GetResult();
            var sqlCreate01 = Conn2.CreateBatchAsync(dataList01).GetAwaiter().GetResult();

            /*
             * AlipayPaymentRecord
             */

            var sqlDelete02 = Conn2.DeleteAsync<AlipayPaymentRecord>(null).GetAwaiter().GetResult();
            var dataList02 = Conn.QueryListAsync<AlipayPaymentRecord>(null).GetAwaiter().GetResult();
            var sqlCreate02 = Conn2.CreateBatchAsync(dataList02).GetAwaiter().GetResult();

        }

    }
}
