using Yunyong.DataExchange;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class MethodParamsTest:TestBase
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

            var xxR1 = "";

            // where method parameter 
            var resR1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => id == it.Id)
                .QueryFirstOrDefaultAsync();

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            Assert.True(res1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));
            Assert.True(resR1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));

            var xx = "";
        }

        [Fact]
        public async Task MethodListParamTest()
        {
            var list = new List<Guid>();
            list.Add(Guid.Parse("00079c84-a511-418b-bd5b-0165442eb30a"));
            list.Add(Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d"));

            await yyy(list);
            await yyy(list.ToArray());
        }
        private async Task yyy(List<Guid> list)
        {
            var xx = "";

            var res = await Conn
                .Selecter<Agent>()
                .Where(it => list.Contains(it.Id))
                .QueryListAsync();

            var xxx = "";
        }
        private async Task yyy(Guid[] arrays)
        {
            var xx = "";

            var res = await Conn
                .Selecter<Agent>()
                .Where(it => arrays.Contains(it.Id))
                .QueryListAsync();

            var xxx = "";
        }

    }
}
