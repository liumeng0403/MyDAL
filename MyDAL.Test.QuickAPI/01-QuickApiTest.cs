using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QuickAPI
{
    public class _01_QuickApiTest : TestBase
    {
        private class AlipayPaymentPaggingQueryOption : PagingQueryOption
        {
            [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
            public DateTime StartTime { get; set; }
        }
        private class AlipayPaymentQueryOption : QueryOption
        {
            [QueryColumn("CreatedOn", CompareEnum.GreaterThanOrEqual)]
            public DateTime StartTime { get; set; }

            public DateTime CreatedOn { get; set; }
        }

        [Fact]
        public async Task Test1()
        {
            /****************************************************************************************/

            var xx1 = "";

            var option1 = new AlipayPaymentPaggingQueryOption();
            option1.StartTime = DateTime.Parse("2018-08-20");
            var res1 = await Conn.QueryPagingListAsync<AlipayPaymentRecord>(option1);
            Assert.True(res1.TotalCount == 29);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx2 = "";

            var res2 = await Conn.QueryPagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1);
            Assert.True(res2.TotalCount == 29);
            Assert.True(res2.Data.Count == 10);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx3 = "";

            var res3 = await Conn.QueryPagingListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option1, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res3.TotalCount == 29);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx4 = "";

            var option4 = new AlipayPaymentQueryOption();
            option4.StartTime = DateTime.Parse("2018-08-20");
            var res4 = await Conn.QueryListAsync<AlipayPaymentRecord>(option4);
            Assert.True(res4.Count == 29);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx5 = "";

            var res5 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4);
            Assert.True(res4.Count == 29);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx6 = "";

            var res6 = await Conn.QueryListAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option4, record => new AlipayPaymentRecordVM
            {
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res4.Count == 29);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx7 = "";

            var pk = Guid.Parse("8f2cbb64-8356-4482-88ee-016558c05b2d");
            var option7 = new AlipayPaymentQueryOption();
            option7.CreatedOn = DateTime.Parse("2018-08-20 19:12:05.933786");
            var res7 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(option7);
            Assert.True(res7.Id == pk);

            var tuple7 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx8 = "";

            var res8 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7);
            Assert.True(res8.Id == pk);

            var tuple8 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx9 = "";

            var res9 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(option7, record => new AlipayPaymentRecordVM
            {
                Id = record.Id,
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.True(res9.Id == pk);

            var tuple9 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx10 = "";

            var res10 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(pk, record => new AlipayPaymentRecordVM
            {
                Id = record.Id,
                TotalAmount = record.TotalAmount,
                Description = record.Description
            });
            Assert.NotNull(res10);

            var tuple10 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx11 = "";

            var res11 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord, AlipayPaymentRecordVM>(pk);
            Assert.NotNull(res11);

            var tuple11 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx12 = "";

            var res12 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(pk);
            Assert.NotNull(res12);

            var tuple12 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /****************************************************************************************/

            var xx13 = "";

            var res13 = await Conn.UpdateAsync<AlipayPaymentRecord>(pk, new
            {
                Description = "xxxxxx"
            });
            Assert.True(res13 == 1);

            var tuple13 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res131 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(pk);
            Assert.True(res131.Description == "xxxxxx");
            
            /****************************************************************************************/

            var xx14 = "";

            var res14 = await Conn.DeleteAsync<AlipayPaymentRecord>(pk);
            Assert.True(res14 == 1);

            var tuple14 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res141 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(pk);
            Assert.Null(res141);

            /****************************************************************************************/

            var xx15 = "";

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

            var res151 = await Conn.QueryFirstOrDefaultAsync<AlipayPaymentRecord>(pk);
            Assert.NotNull(res151);

            /****************************************************************************************/

            var xx16 = "";

            var json = File.ReadAllText(@"C:\Users\liume\Desktop\นคื๗\DalTestDB\ProfileData.json");
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

            var xx = "";

        }
    }
}
