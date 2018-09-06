using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class WhereTest:TestBase
    {

        [Fact]
        public async Task MethodParamTest()
        {
            var res = await Conn
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120"))
                .QueryFirstOrDefaultAsync();

            await xxx(res.Id);
            var id = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            await xxx(id);
        }
        private async Task xxx(Guid id)
        {
            var xx1 = "";

            // where method parameter 
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Id == id)
                .QueryFirstOrDefaultAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";
        }

    }
}
