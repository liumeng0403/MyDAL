using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _01_FirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var time1 = DateTime.Parse("2018-08-16 19:22:01.716307");
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn == time1)
                .FirstOrDefaultAsync<Guid>(it => it.Id);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var xx = "";
        }
    }
}
