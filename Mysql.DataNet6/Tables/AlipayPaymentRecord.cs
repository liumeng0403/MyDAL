using MyDAL;

namespace Mysql.Data_Net6.Tables;

[XTable(Name = "AlipayPaymentRecord")]     // XTableAttribute 指明 该 Model 对应 DB 中的 table 名.
public class AlipayPaymentRecord
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid PaymentRecordId { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Description { get; set; }
    public string PaymentSN { get; set; }
    public DateTime? PayedOn { get; set; }
    public DateTime? CanceledOn { get; set; }
    public string PaymentUrl { get; set; }
}