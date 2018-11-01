using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _03_WhereBoolDefault : TestBase
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

            var xx5 = "";

            var guid5 = Guid.Parse("08d6036c-66c8-7c2c-83b0-725f93ff8137");
            var res5 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address5, out var address55)
                .From(() => address5)
                    .InnerJoin(() => address55)
                        .On(() => address5.Id == address55.Id)
                .Where(() => address5.IsDefault && address5.UserId == guid5)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res5.Count == 1);
            Assert.True(res5.First().IsDefault);

            var res51 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address51, out var address511)
                .From(() => address51)
                    .InnerJoin(() => address511)
                        .On(() => address51.Id == address511.Id)
                .Where(() => address51.IsDefault == true && address51.UserId == guid5)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res51.Count == 1);
            Assert.True(res51.First().IsDefault);

            var guid52 = Guid.Parse("6f390324-2c07-40cf-90ca-0165569461b1");
            var res52 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address52, out var address521)
                .From(() => address52)
                    .InnerJoin(() => address521)
                        .On(() => address52.Id == address521.Id)
                .Where(() => address52.IsDefault == false || address52.Id == guid52)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res52.Count == 3);
            Assert.True(res52.First(it => it.Id != guid52).IsDefault == false);
            Assert.True(res52.First(it => it.Id == guid52).IsDefault);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************************/

            var xx = "";

        }
    }
}
