using EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange;
using EasyDAL.Exchange.Tests.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace EasyDAL.Exchange.Tests
{
    public class _04_QueryFirstOrDefaultTest:TestBase
    {

        private async Task<BodyFitRecord> PreQuery()
        {


            var m = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58"),
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "xxxx"
            };

            // 清理数据
            var resd = await Conn
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == m.Id)
                .DeleteAsync();

            // 造数据
            var resc = await Conn
                .Creater<BodyFitRecord>()
                .CreateAsync(m);

            return m;
        }

        [Fact]
        public async Task Test01()
        {
            var m = PreQuery();

            /****************************************************************************************************************************************/

            var xx1 = "";

            //  == Guid
            var res1 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR1);
            Assert.True(res1.Id == resR1.Id);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx2 = "";

            // == DateTime
            var res2 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters);

            var resR2 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR2);
            Assert.True(res2.Id == resR2.Id);

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);
            
            /****************************************************************************************************************************************/

            var xx3 = "";

            // == string
            var res3 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res3);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var resR3 = await Conn.OpenDebug()
                .Selecter<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR3);
            Assert.True(res3.Id == resR3.Id);

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx4 = "";

            // where and
            var res4 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.DateTime_大于等于)
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res4);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.OpenDebug()
                .Selecter<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync<AgentVM>();
            Assert.NotNull(res5);
            Assert.Null(res5.XXXX);

            var tuple5 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx6 = "";

            var guid6 = Guid.Parse("544b9053-322e-4857-89a0-0165443dcbef");
            var res6 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent6, out var record6)
                .From(() => agent6)
                .InnerJoin(() => record6)
                .On(() => agent6.Id == record6.AgentId)
                .Where(() => agent6.Id == guid6 )
                .QueryFirstOrDefaultAsync<Agent>();
            Assert.NotNull(res6);
            Assert.Equal("夏明君", res6.Name);

            var tuple6 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx7 = "";

            var res7 = await Conn.OpenDebug()
                .Joiner<Agent, AgentInventoryRecord>(out var agent7, out var record7)
                .From(() => agent7)
                .InnerJoin(() => record7)
                .On(() => agent7.Id == record7.AgentId)
                .Where(() => agent7.Id == guid6)
                .QueryFirstOrDefaultAsync(() => new AgentVM
                {
                    nn = agent7.PathId,
                    yy = record7.Id,
                    xx = agent7.Id,
                    zz = agent7.Name,
                    mm = record7.LockedCount
                });
            Assert.NotNull(res7);
            Assert.Equal("夏明君", res7.zz);

            var tuple7 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx = "";

        }

    }
}
