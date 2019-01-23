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
        public async Task Test()
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



            /************************************************************************************************************************************/

            xx = string.Empty;

        }

    }
}
