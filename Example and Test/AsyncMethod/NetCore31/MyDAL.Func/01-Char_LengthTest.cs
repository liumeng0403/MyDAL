﻿using MyDAL.Test.Entities.MyDAL_TestDB;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _01_Char_LengthTest : TestBase
    {

        [Fact]
        public void Char_LengthTest()
        {

            /*
             *char_length
             */
            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.Name.Length > 2)
                .SelectList();
            Assert.True(res1.Count == 22660);

            

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var resR1 = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => 2 < it.Name.Length)
                .SelectList();
            Assert.True(res1.Count == resR1.Count);
            Assert.True(res1.Count == 22660);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res2 = MyDAL_TestDB
                .Selecter(out Agent agent2, out AgentInventoryRecord record2)
                .From(() => agent2)
                    .InnerJoin(() => record2)
                        .On(() => agent2.Id == record2.AgentId)
                .Where(() => agent2.Name.Length > 2)
                .SelectList<Agent>();
            Assert.True(res2.Count == 457);

            

            /************************************************************************************************************************/

            xx = string.Empty;

            // .Where(a => a.Name.Length > 0)
            var res3 = MyDAL_TestDB
                .Selecter(out Agent agent3, out AgentInventoryRecord record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => agent3.Name.Length > 2)
                .OrderBy(() => agent3.Name.Length)
                .SelectList<Agent>();
            Assert.True(res3.Count == 457);

            

            /************************************************************************************************************************/

            xx = string.Empty;
        }


    }
}
