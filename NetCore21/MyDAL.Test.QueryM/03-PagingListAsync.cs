using MyDAL.Test.Entities.MyDAL_TestDB;
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
        public async Task Test()
        {

            /******************************************************************************************************/

            xx = string.Empty;

            // order by
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)128)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync(1, 10);
            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************/

            xx = string.Empty;

            // key
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == (AgentLevel)2)
                .PagingListAsync(1, 10);
            Assert.True(res2.TotalPage == 2807);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************/

            var xx3 = string.Empty;

            // none key
            var res3 = await Conn
                .Queryer<WechatPaymentRecord>()
                .Where(it => it.Amount > 1)
                .PagingListAsync(1, 10);
            Assert.True(res3.TotalPage == 56);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /******************************************************************************************************/

            var xx4 = string.Empty;

            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .PagingListAsync(1, 10);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR4 = await Conn
                .Queryer<Agent>()
                .Where(it => WhereTest.CreatedOn <= it.CreatedOn)
                .PagingListAsync(1, 10);
            Assert.True(res4.TotalCount == resR4.TotalCount);
            Assert.True(res4.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /*************************************************************************************************************************/

        }
    }
}
