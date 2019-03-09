using MyDAL.Test.Entities.MyDAL_TestDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _03_ShortcutAPI
        : TestBase
    {

        [Fact]
        public async Task CreateAsync_Shortcut()
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
            await Conn.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            // 新增一条数据: AlipayPaymentRecord
            var res1 = await Conn.CreateAsync(m);

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            Assert.NotNull(res11);

            /****************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task CreateBatchAsync_Shortcut()
        {

            xx = string.Empty;

            var json = File.ReadAllText(@"C:\Users\liume\Desktop\Work\DalTestDB\ProfileData.json");

            var list = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list)
            {
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
            }

            var res1 = await Conn.CreateBatchAsync(list);

            Assert.True(list.Count == res1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);


            xx = string.Empty;
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
            await Conn.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

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
            var res1 = await Conn.ExecuteNonQueryAsync(sql, paras);

            Assert.True(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            xx = string.Empty;

            var res11 = await Conn.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            Assert.NotNull(res11);

        }

    }
}
