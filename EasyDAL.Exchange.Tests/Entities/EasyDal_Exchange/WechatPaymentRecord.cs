using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyDAL.Exchange.Tests.Entities.EasyDal_Exchange
{
    [Table("WechatPaymentRecord")]
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
