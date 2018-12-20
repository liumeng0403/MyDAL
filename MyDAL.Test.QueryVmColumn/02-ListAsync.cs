﻿using MyDAL.Test.Entities;
using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _02_ListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx = string.Empty;

            var testQ4 = new WhereTestModel
            {
                CreatedOn = DateTime.Now.AddDays(-30),
                StartTime = WhereTest.CreatedOn,
                EndTime = DateTime.Now,
                AgentLevelXX = AgentLevel.DistiAgent,
                ContainStr = "~00-d-3-1-"
            };
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= testQ4.StartTime)
                .ListAsync<AgentVM>();
            Assert.True(res4.Count == 28619);
            Assert.NotNull(res4.First().Name);
            Assert.Null(res4.First().XXXX);

            var tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .ListAsync(agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res5.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}
