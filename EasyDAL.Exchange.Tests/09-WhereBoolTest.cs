using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyDAL.Exchange.Tests
{
    public class WhereBoolTest:TestBase
    {

        [Fact]
        public async Task BoolDefaultTest()
        {

            var xx1 = "";

            // where 1=1
            var res1 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => false) // true  false
                .QueryListAsync();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault)  //  false  none(true)  
                .QueryListAsync();

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }

    }
}
