using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryAPI
{
    public class _01_QueryOneAsync
        : TestBase
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
            var resd = await Conn.DeleteAsync<BodyFitRecord>(it => it.Id == m.Id);

            // 造数据
            var resc = await Conn.CreateAsync(m);

            return m;
        }

        [Fact]
        public async Task History_01()
        {

            await PreQuery();

            /****************************************************************************************************************************************/

            xx = string.Empty;

            //  == Guid
            var res1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .QueryOneAsync();

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR1 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e") == it.Id)
                .QueryOneAsync();

            Assert.NotNull(resR1);
            Assert.True(res1.Id == resR1.Id);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == DateTime
            var res2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.CreatedOn == Convert.ToDateTime("2018-08-23 13:36:58"))
                .QueryOneAsync();

            Assert.NotNull(res2);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR2 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => Convert.ToDateTime("2018-08-23 13:36:58") == it.CreatedOn)
                .QueryOneAsync();

            Assert.NotNull(resR2);
            Assert.True(res2.Id == resR2.Id);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

            // == string
            var res3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => it.BodyMeasureProperty == "xxxx")
                .QueryOneAsync();

            Assert.NotNull(res3);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var resR3 = await Conn
                .Queryer<BodyFitRecord>()
                .Where(it => "xxxx" == it.BodyMeasureProperty)
                .QueryOneAsync();

            Assert.NotNull(resR3);
            Assert.True(res3.Id == resR3.Id);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

        }

        [Fact]
        public async Task History_02()
        {

        }

        [Fact]
        public async Task QuerySingleColumn_Shortcut()
        {
            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, Guid>(it => it.Id == pk && it.CreatedOn == date, it => it.Id);

            Assert.True(res1 == pk);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == pk && it.CreatedOn == date);

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVM_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date);

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryVMColumn_Shortcut()
        {

            xx = string.Empty;

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var date = DateTime.Parse("2018-08-20 19:12:05.933786");

            var res1 = await Conn.QueryOneAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(it => it.Id == pk && it.CreatedOn == date,
            it => new AlipayPaymentRecordVM
            {
                Id = it.Id,
                TotalAmount = it.TotalAmount,
                Description = it.Description
            });

            Assert.NotNull(res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QuerySingleColumn_ST()
        {
            xx = string.Empty;
            
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn == DateTime.Parse("2018-08-16 19:22:01.716307"))
                .QueryOneAsync(it => it.Id);

            Assert.True(res1.Equals(Guid.Parse("000f5f16-5502-4324-b5d6-016544300263")));

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;
        }

        [Fact]
        public async Task QueryM_ST()
        {

            xx = string.Empty;

            // 
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30))
                    .And(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync();

            Assert.NotNull(res4);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }

        [Fact]
        public async Task QueryVM_ST()
        {

        }

        [Fact]
        public async Task QueryVMColumn_ST()
        {

        }

        [Fact]
        public async Task QuerySingleColumn_MT()
        {

        }

        [Fact]
        public async Task QueryM_MT()
        {

        }

        [Fact]
        public async Task QueryVMColumn_MT()
        {

        }

        [Fact]
        public async Task QuerySingleColumn_SQL()
        {

        }

        [Fact]
        public async Task QueryVM_SQL()
        {

        }

    }
}
