using MyDAL.Test.Entities.MyDAL_TestDB;
using System;

namespace MyDAL.Test.DataRecovery
{
    public class _01_MySqlToSqlServer
        :TestBase
    {
        
        public void MySqlToSqlServer2008R2Plus()
        {

            PrintLog("--------------------------------------------------------------------------------------开始");

            /*
             * AddressInfo
             */

            var sqlDelete01 = Conn2.DeleteAsync<AddressInfo>(null).GetAwaiter().GetResult();
            PrintLog($"清理 sql server AddressInfo 表：{sqlDelete01}");
            var dataList01 = Conn.QueryListAsync<AddressInfo>(null).GetAwaiter().GetResult();
            PrintLog($"查询 my sql AddressInfo 表：{dataList01.Count}");
            var sqlCreate01 = Conn2.CreateBatchAsync(dataList01).GetAwaiter().GetResult();
            PrintLog($"恢复 sql server AddressInfo 表：{sqlCreate01}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * AlipayPaymentRecord
             */

            var sqlDelete02 = Conn2.DeleteAsync<AlipayPaymentRecord>(null).GetAwaiter().GetResult();
            PrintLog($"清理 sql server AlipayPaymentRecord 表：{sqlDelete02}");
            var dataList02 = Conn.QueryListAsync<AlipayPaymentRecord>(null).GetAwaiter().GetResult();
            PrintLog($"查询 my sql AlipayPaymentRecord 表：{dataList02.Count}");
            var sqlCreate02 = Conn2.CreateBatchAsync(dataList02).GetAwaiter().GetResult();
            PrintLog($"恢复 sql server AlipayPaymentRecord 表：{sqlCreate02}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * Agent
             */

            var sqlDelete03 = Conn2.DeleteAsync<Agent>(null).GetAwaiter().GetResult();
            PrintLog($"清理 sql server Agent 表：{sqlDelete03}");
            var dataList03 = Conn.QueryListAsync<Agent>(null).GetAwaiter().GetResult();
            PrintLog($"查询 my sql Agent 表：{dataList03.Count}");
            var sqlCreate03 = Conn2.CreateBatchAsync(dataList03).GetAwaiter().GetResult();
            PrintLog($"恢复 sql server Agent 表：{sqlCreate03}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * AgentInventoryRecord
             */

            var sqlDelete04 = Conn2.DeleteAsync<AgentInventoryRecord>(null).GetAwaiter().GetResult();
            PrintLog($"清理 sql server AgentInventoryRecord 表：{sqlDelete04}");
            var dataList04 = Conn.QueryListAsync<AgentInventoryRecord>(null).GetAwaiter().GetResult();
            PrintLog($"查询 my sql AgentInventoryRecord 表：{dataList04.Count}");
            var sqlCreate04 = Conn2.CreateBatchAsync(dataList04).GetAwaiter().GetResult();
            PrintLog($"恢复 sql server AgentInventoryRecord 表：{sqlCreate04}");

            PrintLog("--------------------------------------------------------------------------------------结束");
            Console.ReadLine();

        }

    }
}
