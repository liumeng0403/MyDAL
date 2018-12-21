using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVmColumn
{
    public class _03_PagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx7 = "";

            var option = new AgentQueryOption();
            option.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option.Name = "樊士芹";
            option.OrderBys = new List<OrderBy>
            {
                new OrderBy
                {
                    Field="Name",
                    Desc=true
                }
            };

            // where method -- option orderby 
            var res7 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Name == "樊士芹")
                .PagingListAsync(option, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res7.Data.Count == 1);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx8 = "";

            var res8 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .PagingListAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            var tuple8 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

            var xx = "";

        }
    }
}