using MyDAL.Test.Entities.MyDAL_TestDB;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _01_FirstOrDefaultAsync : TestBase
    {
        [Fact]
        public async Task test()
        {

            await PreQuery();

            /****************************************************************************************************************************************/

            var xx = string.Empty;

            //  == Guid
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .FirstOrDefaultAsync();
            Assert.NotNull(res1);

            var tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .FirstOrDefaultAsync();
            Assert.NotNull(resR1);
            Assert.True(res1.Id == resR1.Id);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == DateTime
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .FirstOrDefaultAsync();
            Assert.NotNull(res2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .FirstOrDefaultAsync();
            Assert.NotNull(resR2);
            Assert.True(res2.Id == resR2.Id);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == string
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .FirstOrDefaultAsync();
            Assert.NotNull(res3);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            var resR3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .FirstOrDefaultAsync();
            Assert.NotNull(resR3);
            Assert.True(res3.Id == resR3.Id);

            var tupleR3 = (XDebug.SQL, XDebug.Parameters);

            /****************************************************************************************************************************************/

            var xx4 = string.Empty;

            // where and
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.CreatedOn)
                    .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .FirstOrDefaultAsync();
            Assert.NotNull(res4);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

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
            var resd = await Conn.DeleteAsync<BodyFitRecord>(it => it.Id == m.Id);

            // 造数据
            var resc = await Conn.CreateAsync(m);

            return m;
        }

    }
}
