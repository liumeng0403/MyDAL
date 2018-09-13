using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
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

        [Fact]
        public async Task JoinBoolDefaultTest()
        {

            var xx1 = "";

            // where 1=1
            var res1 = await Conn.OpenDebug()
                .Joiner<Agent,AgentInventoryRecord>(out var agent,out var record)
                .From(()=>agent)
                .InnerJoin(()=>record).On(()=>agent.Id==record.AgentId)
                .Where(() => true) // true  false
                .QueryListAsync<Agent>();

            var tuple1 = (XDebug.SQL, XDebug.Parameters);


            var xx2 = "";

            var res2 = await Conn.OpenDebug()
                .Joiner<AddressInfo, AddressInfo>(out var address,out var address2)
                .From(() => address)
                .InnerJoin(() => address2).On(() => address.Id == address2.Id)
                .Where(() => address.IsDefault)  //  false  none(true)  
                .QueryListAsync<AddressInfo>(); 

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var xx = "";

        }

    }
}
