using MyDAL.Test.Entities.MyDAL_TestDB;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _09_CountAsync:TestBase
    {
        [Fact]
        public async Task Test()
        {
            var xx1 = string.Empty;

            var res1 = await Conn.CountAsync<Agent>(it => it.Name.Length > 3);
            Assert.True(res1 == 116);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx=string.Empty;
        }
    }
}
