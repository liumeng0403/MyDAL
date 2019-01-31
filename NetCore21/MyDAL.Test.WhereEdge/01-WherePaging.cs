using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
using MyDAL.Test.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _01_WherePaging
        : TestBase
    {

        [Fact]
        public async Task Test01()
        {

            /*
             * 单表
             */

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op1 = new Single_PagingEdgeOption();
            op1.Phone = "19900000218";

            // Where --> PagingListAsync
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(op1)
                .PagingListAsync();

            Assert.True(res1.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op2 = new Single_PagingEdgeOption();
            op2.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> PagingListAsync
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(op2)
                .OrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync();

            Assert.True(res2.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op3 = new Single_PagingEdgeOption();
            op3.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> PagingListAsync
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(op3)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .PagingListAsync();

            Assert.True(res3.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op4 = new Single_PagingEdgeOption();
            op4.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> ... ... --> PagingListAsync
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(op4)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name)
                    .ThenOrderBy(it => it.PathId)
                .PagingListAsync();

            Assert.True(res4.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op5 = new Single_PagingEdgeOption();
            op5.PhoneNotEqual = "19900000218";

            // Where --> Distinct --> PagingListAsync
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(op5)
                .Distinct()
                .PagingListAsync(it => it.AgentLevel);

            Assert.True(res5.TotalCount == 3);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op6 = new Single_PagingEdgeOption();
            op6.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> Distinct --> PagingListAsync
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(op6)
                .OrderBy(it => it.Name)
                .Distinct()
                .PagingListAsync();

            Assert.True(res6.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op7 = new Single_PagingEdgeOption();
            op7.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> Distinct --> PagingListAsync
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(op7)
                .OrderBy(it => it.AgentLevel)
                    .ThenOrderBy(it => it.Name, OrderByEnum.Asc)
                .Distinct()
                .PagingListAsync<AgentVM>();

            Assert.True(res7.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

            var op8 = new Single_PagingEdgeOption();
            op8.PhoneNotEqual = "19900000218";

            // Where --> OrderBy --> ThenOrderBy --> ... ... --> Distinct --> PagingListAsync
            var res8 = await Conn
                .Queryer<Agent>()
                .Where(op8)
                .OrderBy(it => it.PathId)
                    .ThenOrderBy(it => it.Name)
                    .ThenOrderBy(it => it.CreatedOn)
                .Distinct()
                .PagingListAsync<AgentVM>(it => new AgentVM
                {
                    XXXX = it.Name,
                    YYYY = it.PathId
                });

            Assert.True(res8.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
