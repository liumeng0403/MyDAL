using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _03_QueryPagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /*************************************************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .QueryPagingListAsync(1, 10);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn
                .Selecter<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .QueryPagingListAsync(1, 10);
            Assert.True(res1.TotalCount == resR1.TotalCount);
            Assert.True(res1.TotalCount == 28619);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

        }
    }
}
