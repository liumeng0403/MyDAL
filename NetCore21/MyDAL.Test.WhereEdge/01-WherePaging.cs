using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Options;
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

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
