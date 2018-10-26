using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.WhereEdge
{
    public class _03_WhereBoolDefault:TestBase
    {
        [Fact]
        public async Task test()
        {

            var xx1 = "";

            // where 1=1
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => false) // true  false
                .QueryListAsync();
            Assert.True(res1.Count == 0);

            var res11 = await Conn
                .Selecter<Agent>()
                .Where(it => true) // true  false
                .QueryListAsync();
            Assert.True(res11.Count == 28620);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res2.Count == 5);
            Assert.True(res2.First().IsDefault);

            var res21 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault == true)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res21.Count == 5);
            Assert.True(res21.First().IsDefault);

            var res22 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault == false)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res22.Count == 2);
            Assert.True(res22.First().IsDefault == false);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************************/

            var xx3 = "";

            // where 1=1
            var res3 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent3, out var record3)
                .From(() => agent3)
                    .InnerJoin(() => record3)
                        .On(() => agent3.Id == record3.AgentId)
                .Where(() => true) // true  false
                .QueryListAsync<Agent>();
            Assert.True(res3.Count == 574);

            var res31 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent31, out var record31)
                .From(() => agent31)
                    .InnerJoin(() => record31)
                        .On(() => agent31.Id == record31.AgentId)
                .Where(() => false) // true  false
                .QueryListAsync<Agent>();
            Assert.True(res31.Count == 0);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address4, out var address44)
                .From(() => address4)
                    .InnerJoin(() => address44)
                        .On(() => address4.Id == address44.Id)
                .Where(() => address4.IsDefault)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res4.Count == 5);
            Assert.True(res4.First().IsDefault);

            var res41 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address41, out var address411)
                .From(() => address41)
                    .InnerJoin(() => address411)
                        .On(() => address41.Id == address411.Id)
                .Where(() => address41.IsDefault == true)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res41.Count == 5);
            Assert.True(res41.First().IsDefault);

            var res42 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address42, out var address421)
                .From(() => address42)
                    .InnerJoin(() => address421)
                        .On(() => address42.Id == address421.Id)
                .Where(() => address42.IsDefault == false)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res42.Count == 2);
            Assert.True(res42.First().IsDefault == false);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************************/

            var xx = "";

        }
    }
}
