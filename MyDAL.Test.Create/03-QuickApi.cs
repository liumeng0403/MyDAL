using MyDAL.Test.Entities.EasyDal_Exchange;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Create
{
    public class _03_QuickApi:TestBase
    {
        [Fact]
        public async Task test()
        {

            /****************************************************************************************/

            var xx15 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var m15 = new AlipayPaymentRecord
            {
                Id = pk,
                CreatedOn = DateTime.Parse("2018-08-20 19:12:05.933786"),
                PaymentRecordId = Guid.Parse("e94f747e-1a6d-4be6-af51-016558c05b29"),
                OrderId = Guid.Parse("f60f08e7-9678-41a8-b4aa-016558c05afc"),
                TotalAmount = 0.010000000000000000000000000000M,
                Description = null,
                PaymentSN = "2018082021001004180510465833",
                PayedOn = DateTime.Parse("2018-08-20 20:36:35.720525"),
                CanceledOn = null,
                PaymentUrl = "https://openapi.xxx?charset=UTF-8&app_id=xxx&biz_content=xxx&charset=UTF-8&format=JSON&method=zzz&return_url=xxx&sign_type=yyy&timestamp=zzz&version=1.0"
            };
            var res15 = await Conn.CreateAsync<AlipayPaymentRecord>(m15);
            Assert.True(res15 == 1);

            var tuple15 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res151 = await Conn.GetAsync<AlipayPaymentRecord>(pk);
            Assert.NotNull(res151);

            /****************************************************************************************/

            var xx16 = "";

            var json = File.ReadAllText(@"C:\Users\liume\Desktop\工作\DalTestDB\ProfileData.json");
            var list16 = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            foreach (var item in list16)
            {
                item.Id = Guid.NewGuid();
                item.CreatedOn = DateTime.Now;
            }
            var res16 = await Conn.CreateBatchAsync<UserInfo>(list16);
            Assert.True(list16.Count == res16);

            var tuple16 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

        }
    }
}
