using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Tests.Entities;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class FuncTest: TestBase
    {

        [Fact]
        public async Task CharLengthTest()
        {

            var xx1 = "";

            // .Where(a => a.Name.Length > 0)
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Name.Length > 2)
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }


        // 查询 单值
        [Fact]
        public async Task CountTest()
        {
            var xx1 = "";

            // count(id)  like "陈%"
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Name.Contains(LikeTest.百分号))
                .Count(it => it.Id)
                .QuerySingleValueAsync<long>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }


    }
}
