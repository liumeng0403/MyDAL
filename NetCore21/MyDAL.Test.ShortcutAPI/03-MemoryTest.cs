using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _03_MemoryTest:TestBase
    {
        [Fact]
        public async Task test()
        {
            xx=string.Empty;

            for(var i=0;i<100;i++)
            {
                var name = "张";
                var res = await Conn
                    .Queryer<Agent>()
                    .Where(it => it.Name.Contains($"{name}%") && it.CreatedOn > WhereTest.CreatedOn || it.AgentLevel == AgentLevel.DistiAgent)
                    .ListAsync();
                Assert.True(res.Count == 2506);
                Thread.Sleep(5);
            }

            var yy = "";
        }
    }
}
