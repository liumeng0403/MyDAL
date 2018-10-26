using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Interfaces;
using MyDAL.Test.Query;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Func
{
    public class _06_ExistTest : TestBase
    {

        // 查询 是否存在
        [Fact]
        public async Task ExistAsyncTest()
        {
            /*****************************************************************************************/

            var xx1 = "";

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .ExistAsync();
            Assert.True(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /*****************************************************************************************/

            var xx = "";
        }

    }
}
