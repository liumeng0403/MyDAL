using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVmColumn
{
    public class _03_PagingListAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            // where method -- option orderby 
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Name == "樊士芹")
                .OrderBy(it => it.Name, OrderByEnum.Desc)
                .PagingListAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });
            Assert.True(res7.Data.Count == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

            var res8 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .PagingListAsync(1, 10, agent => new AgentVM
                {
                    XXXX = agent.Name,
                    YYYY = agent.PathId
                });

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}