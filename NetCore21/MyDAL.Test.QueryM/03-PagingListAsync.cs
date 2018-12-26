using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _03_PagingListAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            /******************************************************************************************************/

            var xx1 = "";

            // order by
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)128)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync(1, 10);
            Assert.True(res1.TotalCount == 555);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx2 = "";

            // key
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .PagingListAsync(1, 10);
            Assert.True(res2.TotalPage == 2807);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx3 = "";

            // none key
            var res3 = await Conn
                .Queryer<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .PagingListAsync(1, 10);
            Assert.True(res3.TotalPage == 56);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /******************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .PagingListAsync(1, 10);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            var resR4 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .PagingListAsync(1, 10);
            Assert.True(res4.TotalCount == resR4.TotalCount);
            Assert.True(res4.TotalCount == 28619);

            var tupleR4 = (XDebug.SQL, XDebug.Parameters);

            /*************************************************************************************************************************/

        }
    }
}
