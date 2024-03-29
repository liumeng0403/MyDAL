﻿using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Compare
{
    public class _04_NotEqual
        :TestBase
    {

        [Fact]
        public async Task NotEqual()
        {
            xx = string.Empty;

            // != --> <>
            var res1 = await MyDAL_TestDB.SelectListAsync<Agent>(it => it.AgentLevel != AgentLevel.Customer);

            Assert.True(res1.Count == 556);

            

            xx = string.Empty;
        }

        [Fact]
        public async Task NotNotEqual()
        {
            xx = string.Empty;

            // !(!=) --> =
            var res1 = await MyDAL_TestDB.SelectListAsync<Agent>(it => !(it.AgentLevel != AgentLevel.Customer));

            Assert.True(res1.Count == 28063 || res1.Count == 28064);   // 此处为test

            

            /***********************************************************************************************************************/

            xx = string.Empty;

            // == --> =
            var res2 = await MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .OrderBy(it => it.CreatedOn)
                .SelectListAsync<AgentVM>();

            Assert.True(res2.Count == 555);

            
            
            /***********************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
