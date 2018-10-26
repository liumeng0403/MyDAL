using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _01_QueryFirstOrDefaultAsync:TestBase
    {
        [Fact]
        public async Task test()
        {

            await PreQuery();

            /****************************************************************************************************************************************/

            var xx1 = "";

            //  == Guid
            var res1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters);

            var resR1 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR1);
            Assert.True(res1.Id == resR1.Id);

            var tupleR1 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx2 = "";

            // == DateTime
            var res2 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR2 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR2);
            Assert.True(res2.Id == resR2.Id);

            var tupleR2 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx3 = "";

            // == string
            var res3 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res3);

            var tuple3 = (XDebug.SQL, XDebug.Parameters);

            var resR3 = await Conn
                .Selecter<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(resR3);
            Assert.True(res3.Id == resR3.Id);

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx4 = "";

            // where and
            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryFirstOrDefaultAsync();
            Assert.NotNull(res4);

            var tuple4 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

        }

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

    }
}
