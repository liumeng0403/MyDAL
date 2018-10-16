using MyDAL;
using MyDAL.Test.Entities.EasyDal_Exchange;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Query
{
    public class _09_WhereBoolTest : TestBase
    {

        [Fact]
        public async Task BoolDefaultTest()
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


            var xx2 = "";

            var res2 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res2.Count == 5);
            Assert.True(res2.First().IsDefault);

            var res21 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault==true)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res21.Count == 5);
            Assert.True(res21.First().IsDefault);

            var res22 = await Conn
                .Selecter<AddressInfo>()
                .Where(it => it.IsDefault == false)  //  false  none(true)  
                .QueryListAsync();
            Assert.True(res22.Count == 2);
            Assert.True(res22.First().IsDefault==false);


            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }

        [Fact]
        public async Task JoinBoolDefaultTest()
        {

            /********************************************************************************************************************************************/

            var xx1 = "";

            // where 1=1
            var res1 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent1, out var record1)
                .From(() => agent1)
                .InnerJoin(() => record1).On(() => agent1.Id == record1.AgentId)
                .Where(() => true) // true  false
                .QueryListAsync<Agent>();
            Assert.True(res1.Count == 574);

            var res11 = await Conn
                .Joiner<Agent, AgentInventoryRecord>(out var agent11, out var record11)
                .From(() => agent11)
                .InnerJoin(() => record11).On(() => agent11.Id == record11.AgentId)
                .Where(() => false) // true  false
                .QueryListAsync<Agent>();
            Assert.True(res11.Count == 0);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            /********************************************************************************************************************************************/

            var xx2 = "";

            var res2 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address1, out var address12)
                .From(() => address1)
                .InnerJoin(() => address12).On(() => address1.Id == address12.Id)
                .Where(() => address1.IsDefault)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res2.Count == 5);
            Assert.True(res2.First().IsDefault);

            var res21 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address2, out var address22)
                .From(() => address2)
                .InnerJoin(() => address22).On(() => address2.Id == address22.Id)
                .Where(() => address2.IsDefault==true)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res21.Count == 5);
            Assert.True(res21.First().IsDefault);

            var res22 = await Conn
                .Joiner<AddressInfo, AddressInfo>(out var address3, out var address32)
                .From(() => address3)
                .InnerJoin(() => address32).On(() => address3.Id == address32.Id)
                .Where(() => address3.IsDefault==false)  //  false  none(true)  
                .QueryListAsync<AddressInfo>();
            Assert.True(res22.Count == 2);
            Assert.True(res22.First().IsDefault==false);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }

    }
}
