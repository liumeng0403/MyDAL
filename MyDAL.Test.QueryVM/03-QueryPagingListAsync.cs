using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.Core;
using Yunyong.DataExchange;

namespace MyDAL.Test.QueryVM
{
    public class _03_QueryPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx6 = "";

            var res6 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            var resR6 = await Conn
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync<AgentVM>(1, 10);
            Assert.True(res6.TotalCount == resR6.TotalCount);
            Assert.True(res6.TotalCount == 28619);

            var tupleR6 = (XDebug.SQL, XDebug.Parameters);


            /*************************************************************************************************************************/

            var xx13 = "";

            var option13 = new AgentQueryOption();
            var res13 = await Conn
                .Selecter<Agent>()
                .Where(option13.GetCondition())
                .QueryPagingListAsync<AgentVM>(option13);
            Assert.True(res13.TotalCount == 28620);
            Assert.True(res13.Data.Count == 10);

            var tuple13 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }
    }
}
