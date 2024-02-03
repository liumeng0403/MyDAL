using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyDAL.WhereEdge
{
    public class _04_WhereMethodParam:TestBase
    {

        [Fact]
        public void MethodParam()
        {
            var pk = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            var res = MyDAL_TestDB.SelectOne<Agent>(it => it.Id == pk);

            Assert.NotNull(res);

            xxx(res.Id);
            var id = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            xxx(id);
        }
        private void xxx(Guid id)
        {
            xx = string.Empty;

            // where method parameter 
            var res1 = MyDAL_TestDB.SelectOne<Agent>(it => it.Id == id);

            Assert.NotNull(res1);

            

            xx = string.Empty;

            // where method parameter 
            var resR1 = MyDAL_TestDB.SelectOne<Agent>(it => id == it.Id);

            Assert.NotNull(resR1);

            

            Assert.True(res1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));
            Assert.True(resR1.Id.Equals(Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120")));

            xx = string.Empty;
        }


        [Fact]
        public void MethodListParam()
        {
            var list = new List<Guid>();
            list.Add(Guid.Parse("00079c84-a511-418b-bd5b-0165442eb30a"));
            list.Add(Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d"));

            yyy(list);
            yyy(list.ToArray());
        }
        private void yyy(List<Guid> list)
        {
            xx=string.Empty;

            var res = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => list.Contains(it.Id))
                .SelectList();
            Assert.True(res.Count == 2);

            xx=string.Empty;
        }
        private void yyy(Guid[] arrays)
        {
            xx=string.Empty;

            var res = MyDAL_TestDB
                .Selecter<Agent>()
                .Where(it => arrays.Contains(it.Id))
                .SelectList();
            Assert.True(res.Count == 2);

            xx=string.Empty;
        }


    }
}
