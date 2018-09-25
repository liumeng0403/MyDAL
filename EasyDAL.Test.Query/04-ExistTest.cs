using EasyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Test.Query
{
    public class _04_ExistTest : TestBase
    {

        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();

            var tuple = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }
        
    }
}
