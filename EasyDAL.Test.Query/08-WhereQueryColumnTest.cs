using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Query
{
    public class _08_WhereQueryColumnTest:TestBase
    {
        [Fact]
        public async Task ColumnTest()
        {
            var xx1 = "";

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            var res1 = await Conn
                .Selecter<Agent>()
                .Where(option1)
                .QueryListAsync();
            Assert.True(res1.Count == 555);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

        }
    }
}
