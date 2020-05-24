using MyDAL.Test.Entities.MyDAL_TestDB;
using System;

namespace MyDAL.Test.DataRecovery
{
    public class _01_MySqlToSqlServer
        : TestBase
    {

        public void MySqlToSqlServer2012SP1Plus()
        {

            PrintLog("--------------------------------------------------------------------------------------开始");

            /*
             * AddressInfo
             */

            var sqlDelete01 = Conn2.Delete<AddressInfo>(it => true);
            PrintLog($"清理 sql server AddressInfo 表：{sqlDelete01}");
            var dataList01 = Conn.QueryList<AddressInfo>(it => true);
            PrintLog($"查询 my sql AddressInfo 表：{dataList01.Count}");
            var sqlCreate01 = Conn2.CreateBatch(dataList01);
            PrintLog($"恢复 sql server AddressInfo 表：{sqlCreate01}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * AlipayPaymentRecord
             */

            var sqlDelete02 = Conn2.Delete<AlipayPaymentRecord>(it => true);
            PrintLog($"清理 sql server AlipayPaymentRecord 表：{sqlDelete02}");
            var dataList02 = Conn.QueryList<AlipayPaymentRecord>(it => true);
            PrintLog($"查询 my sql AlipayPaymentRecord 表：{dataList02.Count}");
            var sqlCreate02 = Conn2.CreateBatch(dataList02);
            PrintLog($"恢复 sql server AlipayPaymentRecord 表：{sqlCreate02}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * Agent
             */

            var sqlDelete03 = Conn2.Delete<Agent>(it => true);
            PrintLog($"清理 sql server Agent 表：{sqlDelete03}");
            var dataList03 = Conn.QueryList<Agent>(it => true);
            PrintLog($"查询 my sql Agent 表：{dataList03.Count}");
            var total03 = StepProcess<Agent>(dataList03, 300, list =>
            {
                var num = Conn2.CreateBatch(list);
                PrintLog($"恢复 sql server Agent 表：{num}");
                return num;
            });
            PrintLog($"恢复 sql server Agent 表：{total03}");

            PrintLog("--------------------------------------------------------------------------------------");

            /*
             * AgentInventoryRecord
             */

            var sqlDelete04 = Conn2.Delete<AgentInventoryRecord>(it => true);
            PrintLog($"清理 sql server AgentInventoryRecord 表：{sqlDelete04}");
            var dataList04 = Conn.QueryList<AgentInventoryRecord>(it => true);
            PrintLog($"查询 my sql AgentInventoryRecord 表：{dataList04.Count}");
            var sqlCreate04 = Conn2.CreateBatch(dataList04);
            PrintLog($"恢复 sql server AgentInventoryRecord 表：{sqlCreate04}");

            PrintLog("--------------------------------------------------------------------------------------结束");
            Console.ReadLine();

        }

    }
}
