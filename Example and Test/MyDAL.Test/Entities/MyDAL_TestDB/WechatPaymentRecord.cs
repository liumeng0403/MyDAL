using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDAL.Test.Entities.MyDAL_TestDB
{
    [XTable(Name ="WechatPaymentRecord")]
    public class WechatPaymentRecord 
    {

        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        
        public Guid PaymentBillId { get; set; }
        

        public double Amount { get; set; }
        
        public DateTime PaymentTime { get; set; }
        
        public string WechatpayTradeNo { get; set; }
    }
}
