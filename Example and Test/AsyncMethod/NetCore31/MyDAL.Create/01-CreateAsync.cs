using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.TestData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Create
{
    public class _01_CreateAsync
        :TestBase
    {

        private async Task PreCreate(BodyFitRecord m)
        {
            // 清除数据

            xx = string.Empty;

            var res2 = await MyDAL_TestDB
                .Deleter<BodyFitRecord>()
                .Where(it => it.Id == Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"))
                .DeleteAsync();
        }
        private async Task ClearData6()
        {
            await MyDAL_TestDB
                .Deleter<Agent>()
                .Where(it => it.Id == Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"))
                .DeleteAsync();
        }
        private async Task ClearData7()
        {
            await MyDAL_TestDB
                .Deleter<Agent>()
                .Where(it => it.Id == Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"))
                .DeleteAsync();
        }

        [Fact]
        public async Task History_01()
        {

            /********************************************************************************************************************************/

            var m2 = new Agent
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                PathId = "x-xx-xxx-xxxx",
                Name = "张三",
                Phone = "18088889999",
                IdCardNo = "No.12345",
                CrmUserId = "yyyyy",
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,   // DateTime?
                ActiveOrderId = null,  // Guid?
                DirectorStarCount = 5
            };

            xx = string.Empty;

            var res2 = await MyDAL_TestDB.InsertAsync(m2);

            Assert.True(res2 == 1);

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            var res5 = await MyDAL_TestDB.InsertAsync(new Agent
            {
                Id = Guid.NewGuid(),
                CreatedOn = Convert.ToDateTime("2018-10-07 17:02:05"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = null,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            });

            

            /********************************************************************************************************************************/

            xx = string.Empty;

            await ClearData6();

            var m6 = new Agent
            {
                Id = Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"),
                CreatedOn = Convert.ToDateTime("2018-10-07 17:02:05"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            };

            var res6 = await MyDAL_TestDB.InsertAsync(m6);

            

            var res61 = await MyDAL_TestDB.SelectOneAsync<Agent>(it => it.Id == Guid.Parse("ea1ad309-56f7-4e3e-af12-0165c9121e9b"));
            Assert.True(res61.AgentLevel == AgentLevel.DistiAgent);

            /********************************************************************************************************************************/

            xx = string.Empty;

            await ClearData7();

            var m7 = new Agent
            {
                Id = Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"),
                CreatedOn = Convert.ToDateTime("2018-08-16 19:34:25.116759"),
                UserId = Guid.NewGuid(),
                PathId = "xx-yy-zz-mm-nn",
                Name = "meng-net",
                Phone = "17600000000",
                IdCardNo = "876987698798",
                CrmUserId = Guid.NewGuid().ToString(),
                AgentLevel = AgentLevel.DistiAgent,
                ActivedOn = null,
                ActiveOrderId = null,
                DirectorStarCount = 1
            };

            var res7 = await MyDAL_TestDB.InsertAsync(m7);

            

            var res71 = await MyDAL_TestDB.SelectOneAsync<Agent>(it => it.Id == Guid.Parse("08d60369-4fc1-e8e0-44dc-435f31635e6d"));
            Assert.True(res71.CreatedOn == Convert.ToDateTime("2018-08-16 19:34:25.116759"));

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task History_02()
        {
            
            var list1 = await new CreateData().PreCreateBatchV2(MyDAL_TestDB);

            xx = string.Empty;

            var res1 = await MyDAL_TestDB.InsertBatchAsync(list1);

            Assert.True(res1 == 10);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task History_03()
        {
            xx = string.Empty;

            var m = new AlipayPaymentRecord
            {
                Id = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d"),
                CreatedOn = DateTime.Parse("2018-08-20 19:12:05.933786"),
                PaymentRecordId = Guid.Parse("e94f747e-1a6d-4be6-af51-016558c05b29"),
                OrderId = Guid.Parse("f60f08e7-9678-41a8-b4aa-016558c05afc"),
                TotalAmount = 0.010000000000000000000000000000M,
                Description = null,
                PaymentSN = "2018082021001004180510465833",
                PayedOn = DateTime.Parse("2018-08-20 20:36:35.720525"),
                CanceledOn = null,
                PaymentUrl = "https://openapi.xxx?charset=UTF-8&app_id=zzz&version=1.0"
            };

            // 删除一条数据: AlipayPaymentRecord
            await MyDAL_TestDB.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            // 新增一条数据: AlipayPaymentRecord
            var res1 = await MyDAL_TestDB.InsertAsync(m);

            Assert.True(res1 == 1);

            

            var res11 = await MyDAL_TestDB.SelectOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);
            Assert.NotNull(res11);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Create_Shotcut()
        {

            var m1 = new BodyFitRecord
            {
                Id = Guid.Parse("1fbd8a41-c75b-45c0-9186-016544284e2e"),
                CreatedOn = DateTime.Now,
                UserId = Guid.NewGuid(),
                BodyMeasureProperty = "{xxx:yyy,mmm:nnn}"
            };
            await PreCreate(m1);

            xx = string.Empty;

            // 新建
            var res1 = await MyDAL_TestDB.InsertAsync(m1);

            Assert.True(res1 == 1);

            

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task Create_Batch_Shortcut()
        {

            var json = File.ReadAllText(@"D:\GitHub\Me\MyDal\DalTestDB\ProfileData.json");
            var list = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                //Thread.Sleep(1);
                item.CreatedOn = DateTime.Now;
            }

            xx = string.Empty;

            Assert.True(!list.Any(it => it.RootUser));
            Assert.True(!list.Any(it => it.InvitedCount > 0));
            Assert.True(!list.Any(it => !string.IsNullOrWhiteSpace(it.ArrangePathId)));
            Assert.True(!list.Any(it => it.IsVIP));
            Assert.True(!list.Any(it => it.IsActived));

            var res4 = await MyDAL_TestDB.InsertBatchAsync(list);

            Assert.True(res4 == list.Count);

            xx = string.Empty;

        }

        [Fact]
        public async Task Create_ST()
        {
            /*
             * 可以使用便捷方法： IDbConnection.CreateAsync<M>(M m)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> CreateAsync<M>(this IDbConnection conn, M m)
             */

            try
            {
                await None();
            }
            catch { }
        }

        [Fact]
        public async Task Create_Batch_ST()
        {
            /*
             * 可以使用便捷方法： IDbConnection.CreateBatchAsync<M>(IEnumerable<M> mList)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList)
             */

            try
            {
                await None();
            }
            catch { }
        }

        [Fact]
        public async Task Create_MT()
        {
            /*
             * 多表连接方式插入表数据，自己写 SQL
             * 然后使用方法： IDbConnection.ExecuteNonQueryAsync(string sql, List<XParam> dbParas = null)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
             */

            try
            {
                await None();
            }
            catch { }
        }

        [Fact]
        public async Task Create_Batch_MT()
        {
            /*
             * 多表连接方式插入表数据，自己写 SQL
             * 然后使用方法： IDbConnection.ExecuteNonQueryAsync(string sql, List<XParam> dbParas = null)
             * 方法命名空间：using MyDAL;
             * 方法描述： async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null)
             */

            try
            {
                await None();
            }
            catch { }
        }

        [Fact]
        public async Task ExecuteNonQueryAsync_SQL()
        {

            xx = string.Empty;

            var m = new AlipayPaymentRecord
            {
                Id = Guid.Parse("3b6f8abc-9735-4f22-b076-01655797af78"),
                CreatedOn = DateTime.Parse("2018-08-20 13:48:03.320317"),
                PaymentRecordId = Guid.Parse("99b4afd3-9442-4556-a4bf-01655797af73"),
                OrderId = Guid.Parse("c18aa87e-3367-4813-952d-01655797af41"),
                TotalAmount = 293.000000000000000000000000000000M,
                Description = null,
                PaymentSN = null,
                PayedOn = null,
                CanceledOn = null,
                PaymentUrl = "https://openapi.alipay.com/gateway.do?charset=UTF-8"
            };

            // 删除一条数据: AlipayPaymentRecord
            await MyDAL_TestDB.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            var sql = @"
                                insert into `alipaypaymentrecord` 
                                (`Id`,`CreatedOn`,`PaymentRecordId`,`OrderId`,`TotalAmount`,`Description`,`PaymentSN`,`PayedOn`,`CanceledOn`,`PaymentUrl`)
                                values 
                                (@Id,@CreatedOn,@PaymentRecordId,@OrderId,@TotalAmount,@Description,@PaymentSN,@PayedOn,@CanceledOn,@PaymentUrl);
                            ";
            var paras = new List<XParam>
            {
                new XParam{ ParamName="Id",ParamValue=m.Id},
                new XParam{ParamName="CreatedOn",ParamValue=m.CreatedOn},
                new XParam{ParamName="PaymentRecordId",ParamValue=m.PaymentRecordId},
                new XParam{ParamName="OrderId",ParamValue=m.OrderId},
                new XParam{ParamName="TotalAmount",ParamValue=m.TotalAmount},
                new XParam{ ParamName="Description",ParamValue=m.Description},
                new XParam{ParamName="PaymentSN",ParamValue=m.PaymentSN},
                new XParam{ParamName="PayedOn",ParamValue=m.PayedOn},
                new XParam{ParamName="CanceledOn",ParamValue=m.CanceledOn},
                new XParam{ParamName="PaymentUrl",ParamValue=m.PaymentUrl}
            };

            // 新增一条数据: AlipayPaymentRecord
            var res1 = await MyDAL_TestDB.InsertAsync(sql, paras);

            Assert.True(res1 == 1);

            

            var res11 = await MyDAL_TestDB.SelectOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);
            Assert.NotNull(res11);

            xx = string.Empty;

        }

        [Fact]
        public async Task CreateAsync_SQL()
        {

            xx = string.Empty;

            var m = new AlipayPaymentRecord
            {
                Id = Guid.Parse("DDED9817-A73B-490F-9289-016558ECB41C"),
                CreatedOn = DateTime.Parse("2018-08-20 13:48:03.320317"),
                PaymentRecordId = Guid.Parse("99b4afd3-9442-4556-a4bf-01655797af73"),
                OrderId = Guid.Parse("c18aa87e-3367-4813-952d-01655797af41"),
                TotalAmount = 293.000000000000000000000000000000M,
                Description = null,
                PaymentSN = null,
                PayedOn = null,
                CanceledOn = null,
                PaymentUrl = "https://openapi.alipay.com/gateway.do?charset=UTF-8"
            };

            // 删除一条数据: AlipayPaymentRecord
            await MyDAL_TestDB.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            var sql = @"
                                insert into `alipaypaymentrecord` 
                                (`Id`,`CreatedOn`,`PaymentRecordId`,`OrderId`,`TotalAmount`,`Description`,`PaymentSN`,`PayedOn`,`CanceledOn`,`PaymentUrl`)
                                values 
                                (@Id,@CreatedOn,@PaymentRecordId,@OrderId,@TotalAmount,@Description,@PaymentSN,@PayedOn,@CanceledOn,@PaymentUrl);
                            ";
            var paras = new List<XParam>
            {
                new XParam{ ParamName="Id",ParamValue=m.Id},
                new XParam{ParamName="CreatedOn",ParamValue=m.CreatedOn},
                new XParam{ParamName="PaymentRecordId",ParamValue=m.PaymentRecordId},
                new XParam{ParamName="OrderId",ParamValue=m.OrderId},
                new XParam{ParamName="TotalAmount",ParamValue=m.TotalAmount},
                new XParam{ ParamName="Description",ParamValue=m.Description},
                new XParam{ParamName="PaymentSN",ParamValue=m.PaymentSN},
                new XParam{ParamName="PayedOn",ParamValue=m.PayedOn},
                new XParam{ParamName="CanceledOn",ParamValue=m.CanceledOn},
                new XParam{ParamName="PaymentUrl",ParamValue=m.PaymentUrl}
            };

            // 新增一条数据: AlipayPaymentRecord
            var res1 = await MyDAL_TestDB.InsertAsync(sql, paras);

            Assert.True(res1 == 1);

            

            var res11 = await MyDAL_TestDB.SelectOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);
            Assert.NotNull(res11);

            xx = string.Empty;

        }
    }
}
