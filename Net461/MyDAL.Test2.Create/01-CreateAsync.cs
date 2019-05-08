using HPC.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDAL.Test;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.TestData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Test2.Create
{
    [TestClass]
    public class _01_CreateAsync
        :TestBase
    {
        
        [TestMethod]
        public async Task Create_Batch_Shortcut()
        {

            var list = await new CreateData().PreCreateBatchV2(Conn2);

            xx = string.Empty;

            var res1 = await Conn2.CreateBatchAsync(list);

            Assert.IsTrue(res1 == 10);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

        }

        [TestMethod]
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
            await Conn2.DeleteAsync<AlipayPaymentRecord>(it => it.Id == m.Id);

            var sql = @"
                                insert into [alipaypaymentrecord]
                                ([Id],[CreatedOn],[PaymentRecordId],[OrderId],[TotalAmount],[Description],[PaymentSN],[PayedOn],[CanceledOn],[PaymentUrl])
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
            var res1 = await Conn2.CreateAsync(sql, paras);

            Assert.IsTrue(res1 == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            var res11 = await Conn2.QueryOneAsync<AlipayPaymentRecord>(it => it.Id == m.Id);
            Assert.IsNotNull(res11);

            xx = string.Empty;

        }
    }
}
