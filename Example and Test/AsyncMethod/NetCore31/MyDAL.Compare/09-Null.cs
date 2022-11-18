using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _09_Null
        : TestBase
    {


        public Agent PreData3()
        {
            var m = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("0001c614-dbef-4335-94b4-01654433a215"))
                .SelectOne();

            MyDAL_TestDB
                .Updater<Agent>()
                .Set(it => it.AgentLevel, WhereTest.AgentLevelNull)
                .Where(it => it.Id == m.Id)
                .Update();

            return m;
        }
        private void ClearData3(Agent m)
        {
            MyDAL_TestDB
                .Updater<Agent>()
                .Set(it => it.AgentLevel, m.AgentLevel)
                .Where(it => it.Id == m.Id)
                .Update();
        }

        [Fact]
        public void WhereTestx()
        {
            xx = string.Empty;

            /************************************************************************************************************************/

            // is not null 
            var res2 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId != null)
                .SelectList();
            Assert.True(res2.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            var m = PreData3();
            // 
            try
            {
                var res3 = MyDAL_TestDB
                    .Selecter<Agent>()
                    .Where(it => it.AgentLevel == WhereTest.AgentLevelNull)
                    .SelectList();
            }
            catch (Exception ex)
            {
                
                var errStr = "【ERR-078】 -- [[[[Convert(value(MyDAL.Compare._09_Null).WhereTest.AgentLevelNull, Nullable`1)]] 中,传入的 SQL 筛选条件为 Null !!!]] ，请 EMail: --> liumeng0403@163.com <--";
                Assert.Equal(errStr, ex.Message, ignoreCase: true);
            }

            ClearData3(m);

            /************************************************************************************************************************/

            var xx4 = string.Empty;

            // is not null 
            var res4 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.ActivedOn != null && it.ActiveOrderId != null && it.CrmUserId == null)
                .SelectList();
            Assert.True(res4.Count == 554);

            

            /************************************************************************************************************************/

            var xx5 = string.Empty;

            // is not null 
            var res5 = MyDAL_TestDB
                .Selecter(out Agent a5, out AgentInventoryRecord r5)
                .From(() => a5)
                    .LeftJoin(() => r5)
                        .On(() => a5.Id == r5.AgentId)
                .Where(() => a5.ActiveOrderId == null)
                .SelectList<Agent>();
            Assert.True(res5.Count == 28085);

            

            /************************************************************************************************************************/

            var res7 = MyDAL_TestDB.SelectList<Agent>(it => it.ActiveOrderId == null);
            Assert.True(res7.Count == 28066);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // is not null 
            var res8 = MyDAL_TestDB.SelectList<Agent>(it => it.ActiveOrderId != null);
            Assert.True(res8.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }


        [Fact]
        public void IsNull()
        {

            xx = string.Empty;

            // is null 
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.ActiveOrderId == null)
                .SelectList();

            Assert.True(res1.Count == 28066);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void NotIsNull()
        {

            xx = string.Empty;

            // !(is null) --> is not null 
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => !(it.ActiveOrderId == null))
                .SelectList();

            Assert.True(res1.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void IsNotNull()
        {

            xx = string.Empty;

            // is not null 
            var res6 = MyDAL_TestDB
                .Selecter(out Agent a6, out AgentInventoryRecord r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => a6.ActiveOrderId != null)
                .SelectList<Agent>();

            Assert.True(res6.Count == 554);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public void NotIsNotNull()
        {

            xx = string.Empty;

            // !(is not null) --> is null 
            var res6 = MyDAL_TestDB
                .Selecter(out Agent a6, out AgentInventoryRecord r6)
                .From(() => a6)
                    .LeftJoin(() => r6)
                        .On(() => a6.Id == r6.AgentId)
                .Where(() => !(a6.ActiveOrderId != null))
                .SelectList<Agent>();

            Assert.True(res6.Count == 28085);

            

            /************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
