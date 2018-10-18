using System;

namespace MyDAL.Test.ViewModels
{
    public class AlipayPaymentRecordVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public DateTime? CanceledOn { get; set; }
    }
}
