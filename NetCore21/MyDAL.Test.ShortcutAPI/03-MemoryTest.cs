using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.ShortcutAPI
{
    public class _03_MemoryTest:TestBase
    {
        [Fact]
        public async Task Test()
        {
            xx=string.Empty;

            for(var i=0;i<100;i++)
            {
                var name = "张";
                var res = await Conn
                    .Queryer<Agent>()
                    .Where(it => it.Name.Contains($"{name}%") && it.CreatedOn > Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30) || it.AgentLevel == AgentLevel.DistiAgent)
                    .QueryListAsync();
                Assert.True(res.Count == 2506);
                Thread.Sleep(5);
            }

            var yy = string.Empty;
        }
    }
}
