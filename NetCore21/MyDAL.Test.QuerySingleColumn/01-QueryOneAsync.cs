using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuerySingleColumn
{
    public class _01_QueryOneAsync
        : TestBase
    {
        [Fact]
        public async Task test()
        {
            xx = string.Empty;

            var time1 = DateTime.Parse("2018-08-16 19:22:01.716307");
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn == time1)
                .QueryOneAsync(it => it.Id);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }
    }
}
