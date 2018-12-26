using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _04_WhereMethodParam:TestBase
    {

        [Fact]
        public async Task MethodParam()
        {
            var pk = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            var res = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == pk);
            Assert.NotNull(res);

            await xxx(res.Id);
            var id = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            await xxx(id);
        }
        private async Task xxx(Guid id)
        {
            xx = string.Empty;

            // where method parameter 
            var res1 = await Conn.FirstOrDefaultAsync<Agent>(it => it.Id == id);
            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            xx = string.Empty;

            // where method parameter 
            var resR1 = await Conn.FirstOrDefaultAsync<Agent>(it => id == it.Id);
            Assert.NotNull(resR1);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            Assert.True(res1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));
            Assert.True(resR1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));

            xx = string.Empty;
        }


        [Fact]
        public async Task MethodListParam()
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
                .Queryer<Agent>()
                .Where(it => list.Contains(it.Id))
                .ListAsync();
            Assert.True(res.Count == 2);

            var xxx = "";
        }
        private async Task yyy(Guid[] arrays)
        {
            var xx = "";

            var res = await Conn
                .Queryer<Agent>()
                .Where(it => arrays.Contains(it.Id))
                .ListAsync();
            Assert.True(res.Count == 2);

            var xxx = "";
        }


    }
}
